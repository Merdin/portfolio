import 'dart:convert';
import 'dart:io';
import 'dart:async';
import 'dart:math';

import 'package:flutter/foundation.dart';
import 'package:http/http.dart' as http;
import 'package:shared_preferences/shared_preferences.dart';

class Auth with ChangeNotifier {
  String? _token;
  DateTime? _expiryDate;
  String? _userId;
  Timer? _authTimer;

  bool get isAuth {
    return token != null;
  }

  String? get token {
    if (_expiryDate?.isAfter(DateTime.now()) != null && _token != null) {
      return _token;
    }

    return null;
  }

  String? get userId {
    if (_userId != null) {
      return _userId;
    }

    return null;
  }

  Future<void> _authenticate(String email, String password, String urlSegment) async {
    final url = Uri.parse(
        "https://identitytoolkit.googleapis.com/v1/accounts:$urlSegment?key=<your-api-key>");

    try {
      final response = await http.post(
        url,
        body: json.encode({
          'email': email,
          'password': password,
          'returnSecureToken': true,
        }),
      );

      final responseData = json.decode(response.body);

      if (responseData['error'] != null) {
        throw HttpException(responseData['error']['message']);
      }

      _token = responseData['idToken'];
      _userId = responseData['localId'];
      _expiryDate = DateTime.now().add(Duration(seconds: int.parse(responseData['expiresIn'])));

      _autoLogout();

      notifyListeners();

      final SharedPreferences prefs = await SharedPreferences.getInstance();
      final userData = json.encode({
        'token': _token,
        'userId': _userId,
        'expiryDate': _expiryDate?.toIso8601String(),
      });
      await prefs.setString("userData", userData);
    } catch (e) {
      rethrow;
    }
  }

  Future<void> signin(String email, String password) async {
    return _authenticate(email, password, "signInWithPassword");
  }

  Future<void> signup(String email, String password) async {
    return _authenticate(email, password, "signUp");
  }

  Future<bool> tryAutoLogin() async {
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    if (!prefs.containsKey('userData')) {
      return false;
    }
    final userData = prefs.getString("userData") as String;
    final extractedUserData = json.decode(userData);

    final expiryDate = DateTime.parse(extractedUserData['expiryDate'] as String);

    if (expiryDate.isBefore(DateTime.now())) {
      return false;
    }
    _token = extractedUserData['token'] as String?;
    _userId = extractedUserData['userId'] as String?;
    _expiryDate = expiryDate;

    notifyListeners();
    _autoLogout();

    return true;
  }

  void logout() async {
    _token = null;
    _userId = null;
    _expiryDate = null;
    if (_authTimer != null) {
      _authTimer?.cancel();
      _authTimer = null;
    }

    // Clear all data that is saved on the device
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    prefs.clear();

    notifyListeners();
  }

  void _autoLogout() {
    if (_authTimer != null) {
      _authTimer?.cancel();
    }

    final timeToExpiry = _expiryDate?.difference(DateTime.now()).inSeconds;
    if (timeToExpiry == null) {
      logout();
      return;
    }

    _authTimer = Timer(Duration(seconds: timeToExpiry), logout);
  }
}

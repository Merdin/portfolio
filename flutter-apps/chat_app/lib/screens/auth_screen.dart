import 'dart:io';

import 'package:flutter/material.dart';
import 'package:firebase_auth/firebase_auth.dart';
import 'package:cloud_firestore/cloud_firestore.dart';
import 'package:firebase_storage/firebase_storage.dart';
import 'package:flutter/services.dart';

import '../widgets/auth/auth_form.dart';

class AuthScreen extends StatefulWidget {
  @override
  State<AuthScreen> createState() => _AuthScreenState();
}

class _AuthScreenState extends State<AuthScreen> {
  final _auth = FirebaseAuth.instance;
  var _isLoading = false;

  void _submitAuthForm(
    String email,
    String password,
    String username,
    File? image,
    bool isLogin,
    BuildContext ctx,
  ) async {
    UserCredential authCredentials;

    try {
      setState(() {
        _isLoading = true;
      });

      if (isLogin) {
        authCredentials = await _auth.signInWithEmailAndPassword(
          email: email,
          password: password,
        );
      } else {
        if (image == null) return;

        authCredentials = await _auth.createUserWithEmailAndPassword(
          email: email,
          password: password,
        );

        final userId = authCredentials.user?.uid;

        if (userId == null) return;

        final ref = FirebaseStorage.instance.ref().child('user_images').child('$userId.jpg');

        String? url;
        await ref.putFile(image).whenComplete(() async {
          url = await ref.getDownloadURL();
        });

        if (url == null) return;

        return await FirebaseFirestore.instance.collection('users').doc(userId).set({
          'username': username,
          'email': email,
          'image_url': url,
        });
      }
    } on PlatformException catch (e) {
      var message = 'An error occured, please check your credentials';

      if (e.message != null) {
        message = e.message!;
      }

      ScaffoldMessenger.of(ctx).showSnackBar(SnackBar(
        content: Text(message),
        backgroundColor: Theme.of(ctx).colorScheme.error,
      ));

      await _auth.signOut();
    } catch (e) {
      await _auth.signOut();
      print(e);
    }

    setState(() {
      _isLoading = false;
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Theme.of(context).primaryColor,
      body: AuthForm(_submitAuthForm, _isLoading),
    );
  }
}

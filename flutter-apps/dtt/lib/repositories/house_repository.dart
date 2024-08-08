import 'package:dio/dio.dart';

import '../models/house_response.dart';

class HouseRepository {
  final String accessKey = "98bww4ezuzfePCYFxJEWyszbUXc7dxRx";
  static String apiUrl = "https://intern.d-tt.nl/api";

  final Dio _dio = Dio();
  var getHousesUrl = '$apiUrl/house';

  Future<HouseResponse> getHouses() async {
    _dio.options.headers = {'Access-Key': accessKey};

    try {
      Response response = await _dio.get(getHousesUrl);
      return HouseResponse.fromJson(response.data);
    } catch (error, stacktrace) {
      print("Exception occured: $error - stacktrace: $stacktrace");
      return HouseResponse.withError("$error");
    }
  }
}

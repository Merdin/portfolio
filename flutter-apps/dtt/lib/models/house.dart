import 'package:geolocator/geolocator.dart';
import 'package:google_maps_flutter/google_maps_flutter.dart';

import '../services/geolocation_service.dart';

class House {
  final int id;
  final String image;
  final int price;
  final int bedrooms;
  final int bathrooms;
  final int size;
  final String description;
  final String zip;
  final String city;
  final int latitude;
  final int longitude;
  final String createdDate;

  House(
    this.id,
    this.image,
    this.price,
    this.bedrooms,
    this.bathrooms,
    this.size,
    this.description,
    this.zip,
    this.city,
    this.latitude,
    this.longitude,
    this.createdDate,
  );

  House.fromJson(Map<String, dynamic> json)
      : id = json["id"],
        image = 'https://intern.d-tt.nl${json["image"]}',
        price = json["price"],
        bedrooms = json["bedrooms"],
        bathrooms = json["bathrooms"],
        size = json["size"],
        description = json["description"],
        zip = json["zip"],
        city = json["city"],
        latitude = json["latitude"],
        longitude = json["longitude"],
        createdDate = json["createdDate"];

  Future<double> get distance async => await getDistanceToHouseInMeters();

  Future<double> getDistanceToHouseInMeters() async {
    final position = await _determinePosition();
    final houseCoordinates = LatLng(latitude.toDouble(), longitude.toDouble());

    return Geolocator.distanceBetween(
          position.latitude,
          position.longitude,
          houseCoordinates.latitude,
          houseCoordinates.longitude,
        ) /
        1000;
  }

  Future<Position> _determinePosition() async {
    bool serviceEnabled = await Geolocator.isLocationServiceEnabled();
    if (!serviceEnabled) {
      return Future.error('Location services are disabled.');
    }

    LocationPermission permission = await Geolocator.checkPermission();
    if (permission == LocationPermission.denied) {
      permission = await Geolocator.requestPermission();
      if (permission == LocationPermission.denied) {
        return Future.error('Location permissions are denied');
      }
    }

    if (permission == LocationPermission.deniedForever) {
      return Future.error(
          'Location permissions are permanently denied, we cannot request permissions.');
    }

    return await Geolocator.getCurrentPosition(
      desiredAccuracy: LocationAccuracy.high,
    );
  }
}

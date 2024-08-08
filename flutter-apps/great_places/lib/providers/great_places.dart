import 'dart:io';

import 'package:flutter/foundation.dart';

import '../models/place.dart';
import '../helpers/db_helper.dart';
import '../helpers/location_helper.dart';

class GreatPlaces with ChangeNotifier {
  List<Place> _items = [];

  List<Place> get items => [..._items];

  Future<void> addPlace(String pickedTitle, File pickedImage, PlaceLocation pickedLocation) async {
    final address = await LocationHelper.getPlaceAddress(pickedLocation.latitude, pickedLocation.longitude);

    final location =
        PlaceLocation(latitude: pickedLocation.latitude, longitude: pickedLocation.longitude, address: address);

    final newPlace = Place(
      id: UniqueKey().toString(),
      image: pickedImage,
      title: pickedTitle,
      location: location,
    );

    _items.add(newPlace);
    notifyListeners();

    DBHelper.insert('user_places', {
      'id': newPlace.id,
      'title': newPlace.title,
      'image': newPlace.image.path,
      'loc_lat': newPlace.location?.latitude,
      'loc_lon': newPlace.location?.longitude,
      'address': newPlace.location?.address
    });
  }

  Future<void> fetchPlaces() async {
    final dataList = await DBHelper.fetchAll('user_places');

    _items = dataList
        .map((item) => Place(
              id: item['id'],
              title: item['title'],
              image: File(item['image']),
              location: PlaceLocation(
                latitude: item['loc_lat'],
                longitude: item['loc_lon'],
                address: item['address'],
              ),
            ))
        .toList();

    notifyListeners();
  }

  Place findById(String id) {
    return _items.firstWhere((item) => item.id == id);
  }
}

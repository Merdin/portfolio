import 'dart:io';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../providers/great_places.dart';

import '../models/place.dart';

import '../widgets/image_input.dart';
import '../widgets/location_input.dart';

class AddPlaceScreen extends StatefulWidget {
  static const identifier = '/add-place';

  @override
  State<AddPlaceScreen> createState() => _AddPlaceScreenState();
}

class _AddPlaceScreenState extends State<AddPlaceScreen> {
  final _titleController = TextEditingController();
  File? _pickedImage;
  PlaceLocation? _pickedLocation;

  void _selectImage(File pickedImage) {
    _pickedImage = pickedImage;
  }

  void _selectPlace(double latitude, double longitude) {
    _pickedLocation = PlaceLocation(latitude: latitude, longitude: longitude);
  }

  void _savePlace() {
    final image = _pickedImage;
    final location = _pickedLocation;
    if (_titleController.text.isEmpty || image == null || location == null) return;

    Provider.of<GreatPlaces>(context, listen: false).addPlace(_titleController.text, image, location);
    Navigator.of(context).pop();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Add a new place'),
      ),
      body: SafeArea(
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.stretch,
          children: [
            Expanded(
              child: SingleChildScrollView(
                child: Padding(
                  padding: const EdgeInsets.all(10.0),
                  child: Column(
                    children: [
                      TextField(
                        decoration: const InputDecoration(labelText: 'Title'),
                        controller: _titleController,
                      ),
                      const SizedBox(height: 10),
                      ImageInput(_selectImage),
                      const SizedBox(height: 10),
                      LocationInput(_selectPlace)
                    ],
                  ),
                ),
              ),
            ),
            ElevatedButton.icon(
              style: ElevatedButton.styleFrom(
                backgroundColor: Theme.of(context).colorScheme.secondary,
                foregroundColor: Colors.black,
              ),
              icon: const Icon(Icons.add),
              label: const Text('Add place'),
              onPressed: _savePlace,
            ),
          ],
        ),
      ),
    );
  }
}

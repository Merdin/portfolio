import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:great_places/providers/great_places.dart';
import 'package:great_places/screens/map_screen.dart';
import 'package:provider/provider.dart';

import '../models/place.dart';

class PlaceDetailScreen extends StatelessWidget {
  static const identifier = '/place-detail';

  @override
  Widget build(BuildContext context) {
    final id = ModalRoute.of(context)?.settings.arguments as String;
    final selectedPlace = Provider.of<GreatPlaces>(context, listen: false).findById(id);

    return Scaffold(
      appBar: AppBar(
        title: Text(selectedPlace.title),
      ),
      body: Column(
        children: [
          SizedBox(
            height: 250,
            width: double.infinity,
            child: Image.file(
              selectedPlace.image,
              fit: BoxFit.cover,
              width: double.infinity,
            ),
          ),
          const SizedBox(height: 10),
          Text(
            selectedPlace.location?.address ?? '',
            textAlign: TextAlign.center,
            style: const TextStyle(fontSize: 20, color: Colors.grey),
          ),
          const SizedBox(height: 10),
          TextButton(
            style: TextButton.styleFrom(foregroundColor: Theme.of(context).primaryColor),
            onPressed: () {
              Navigator.of(context).push(
                MaterialPageRoute(
                  fullscreenDialog: true,
                  builder: (ctx) => MapScreen(
                    initialLocation: selectedPlace.location ??
                        const PlaceLocation(
                          latitude: 52.5125,
                          longitude: 6.09444,
                        ),
                  ),
                ),
              );
            },
            child: const Text('View on Map'),
          )
        ],
      ),
    );
  }
}

import 'package:flutter/material.dart';
import 'package:great_places/screens/place_detail_screen.dart';
import 'package:provider/provider.dart';

import '../screens/add_place_screen.dart';

import '../providers/great_places.dart';

class PlacesListScreen extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Your Places'),
        actions: [
          IconButton(
            icon: const Icon(Icons.add),
            onPressed: () {
              Navigator.of(context).pushNamed(AddPlaceScreen.identifier);
            },
          )
        ],
      ),
      body: FutureBuilder(
        future: Provider.of<GreatPlaces>(context, listen: false).fetchPlaces(),
        builder: (context, snapshot) => snapshot.connectionState == ConnectionState.waiting
            ? const Center(
                child: CircularProgressIndicator(),
              )
            : Consumer<GreatPlaces>(
                child: const Center(
                  child: Text('Got no places yet, start adding some!'),
                ),
                builder: (context, greatPlaces, ch) => greatPlaces.items.isEmpty
                    ? ch!
                    : ListView.builder(
                        itemCount: greatPlaces.items.length,
                        itemBuilder: (ctx, i) => ListTile(
                          leading: CircleAvatar(
                            backgroundImage: FileImage(
                              greatPlaces.items[i].image,
                            ),
                          ),
                          title: Text(greatPlaces.items[i].title),
                          subtitle: Text(greatPlaces.items[i].location?.address ?? ''),
                          onTap: () {
                            Navigator.of(context).pushNamed(
                              PlaceDetailScreen.identifier,
                              arguments: greatPlaces.items[i].id,
                            );
                          },
                        ),
                      ),
              ),
      ),
    );
  }
}

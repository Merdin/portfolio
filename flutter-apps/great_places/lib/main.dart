import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import './providers/great_places.dart';

import './screens/place_detail_screen.dart';
import './screens/places_list_screen.dart';
import './screens/add_place_screen.dart';

void main() => runApp(MyApp());

class MyApp extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return ChangeNotifierProvider.value(
      value: GreatPlaces(),
      child: MaterialApp(
        title: 'Great Places',
        theme: ThemeData(
          primarySwatch: Colors.indigo,
          colorScheme: ColorScheme.fromSwatch(
            accentColor: Colors.amber,
          ),
        ),
        home: PlacesListScreen(),
        routes: {
          AddPlaceScreen.identifier: (ctx) => AddPlaceScreen(),
          PlaceDetailScreen.identifier: (ctx) => PlaceDetailScreen(),
        },
      ),
    );
  }
}

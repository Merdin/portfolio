import 'package:dtt/models/house_response.dart';
import 'package:dtt/services/geolocation_service.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

import './presentation/screens/tab_bar_screen.dart';
import './presentation/screens/house_overview_screen.dart';
import './presentation/screens/house_detail_screen.dart';

void main() {
  runApp(MyApp());
}

class MyApp extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Flutter Demo',
      theme: ThemeData(
        primarySwatch: Colors.blue,
      ),
      home: TabBarScreen(),
      routes: {
        HouseOverviewScreen.identifier: (ctx) => HouseOverviewScreen(),
        HouseDetailScreen.identifier: (ctx) => HouseDetailScreen(),
      },
    );
  }
}

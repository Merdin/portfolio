import 'package:flutter/material.dart';

import '../screens/house_overview_screen.dart';
import '../screens/about_screen.dart';

class TabBarScreen extends StatefulWidget {
  @override
  State<TabBarScreen> createState() => _TabBarScreenState();
}

class _TabBarScreenState extends State<TabBarScreen> {
  int _selectedIndex = 0;

  void _barItemTapped(int index) {
    setState(() {
      _selectedIndex = index;
    });
  }

  Widget? body() {
    switch (_selectedIndex) {
      case 0:
        return HouseOverviewScreen();
      case 1:
        return AboutScreen();
      default:
        return HouseOverviewScreen();
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: body(),
      bottomNavigationBar: BottomNavigationBar(
        showSelectedLabels: false,
        showUnselectedLabels: false,
        items: const [
          BottomNavigationBarItem(icon: Icon(Icons.home), label: ""),
          BottomNavigationBarItem(icon: Icon(Icons.info), label: ""),
        ],
        currentIndex: _selectedIndex,
        selectedItemColor: Colors.black,
        onTap: _barItemTapped,
      ),
    );
  }
}

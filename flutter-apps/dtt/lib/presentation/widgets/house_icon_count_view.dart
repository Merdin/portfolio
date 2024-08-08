import 'package:flutter/material.dart';

class HouseIconCountView extends StatelessWidget {
  final int bedrooms;
  final int bathrooms;
  final int size;
  final String distance;

  HouseIconCountView({
    required this.bedrooms,
    required this.bathrooms,
    required this.size,
    required this.distance,
  });

  @override
  Widget build(BuildContext context) {
    return Wrap(
      spacing: 15,
      alignment: WrapAlignment.spaceBetween,
      children: [
        IconCount(Icons.bed_outlined, bedrooms.toString()),
        IconCount(Icons.shower_outlined, bathrooms.toString()),
        IconCount(Icons.square_foot, size.toString()),
        IconCount(Icons.location_on_outlined, "$distance km"),
      ],
    );
  }
}

class IconCount extends StatelessWidget {
  final IconData icon;
  final String countText;
  final double iconSize;

  IconCount(this.icon, this.countText, {this.iconSize = 15.0});

  @override
  Widget build(BuildContext context) {
    final fontSize = iconSize - 6;
    return Row(
      mainAxisAlignment: MainAxisAlignment.center,
      crossAxisAlignment: CrossAxisAlignment.center,
      children: [
        Icon(icon, size: iconSize),
        const SizedBox(width: 4),
        Text(
          countText,
          style: TextStyle(fontSize: fontSize),
        ),
      ],
    );
  }
}

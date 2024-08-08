import 'package:flutter/material.dart';

import '../screens/house_detail_screen.dart';
import '../widgets/house_icon_count_view.dart';

class HouseOverviewListItem extends StatelessWidget {
  final int id;
  final String imageUrl;
  final String price;
  final String address;

  final int bedrooms;
  final int bathrooms;
  final int size;
  final String distance;

  HouseOverviewListItem({
    required this.id,
    required this.imageUrl,
    required this.price,
    required this.address,
    required this.bedrooms,
    required this.bathrooms,
    required this.size,
    required this.distance,
  });

  @override
  Widget build(BuildContext context) {
    const rowHeight = 70.0;

    return InkWell(
      onTap: () {
        Navigator.of(context)
            .pushNamed(HouseDetailScreen.identifier, arguments: id);
      },
      child: Card(
        elevation: 1,
        child: Padding(
          padding: const EdgeInsets.all(15.0),
          child: Row(children: [
            ClipRRect(
              borderRadius: BorderRadius.circular(10.0),
              child: Container(
                height: rowHeight,
                width: rowHeight,
                color: Colors.black,
                child: Image.network(
                  imageUrl,
                  fit: BoxFit.cover,
                ),
              ),
            ),
            const SizedBox(width: 18),
            SizedBox(
              height: rowHeight,
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [
                  Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      Text(
                        "\$$price",
                        style: const TextStyle(
                          fontSize: 18,
                          fontWeight: FontWeight.w600,
                        ),
                      ),
                      Text(
                        address,
                        style: const TextStyle(
                            fontSize: 12, fontWeight: FontWeight.w300),
                      ),
                    ],
                  ),
                  HouseIconCountView(
                    bedrooms: bedrooms,
                    bathrooms: bathrooms,
                    size: size,
                    distance: distance,
                  )
                ],
              ),
            )
          ]),
        ),
      ),
    );
  }
}

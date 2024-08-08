import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:google_maps_flutter/google_maps_flutter.dart';

import '../../blocs/get_houses_bloc.dart';
import '../widgets/google_maps_view.dart';
import '../../models/house_response.dart';
import '../widgets/house_icon_count_view.dart';

class HouseDetailScreen extends StatelessWidget {
  static const identifier = "/house-detail";

  @override
  Widget build(BuildContext context) {
    final houseId = ModalRoute.of(context)?.settings.arguments as int;

    return StreamBuilder<HouseResponse>(
        stream: housesBloc.subject.stream,
        builder: (context, snapshot) {
          final house = snapshot.data!.getHouse(houseId);
          return Scaffold(
            extendBodyBehindAppBar: true,
            appBar: AppBar(
              backgroundColor: Colors.transparent,
              elevation: 0.0,
            ),
            body: SingleChildScrollView(
              child: Column(children: [
                Image.network(
                  house.image,
                  fit: BoxFit.cover,
                  width: double.infinity,
                  height: 300,
                ),
                Padding(
                  padding: const EdgeInsets.all(25.0),
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      Row(
                        mainAxisAlignment: MainAxisAlignment.spaceBetween,
                        children: [
                          Text(
                            '\$${house.price}',
                            style: const TextStyle(
                              fontSize: 18,
                              fontWeight: FontWeight.bold,
                            ),
                          ),
                          FutureBuilder(
                            future: house.distance,
                            builder: (context, snapshot) => HouseIconCountView(
                              bedrooms: house.bedrooms,
                              bathrooms: house.bathrooms,
                              size: house.size,
                              distance:
                                  snapshot.data?.toStringAsFixed(2) ?? "N/A",
                            ),
                          ),
                        ],
                      ),
                      const SizedBox(height: 30),
                      const Text(
                        "Description",
                        style: TextStyle(
                          fontSize: 18,
                          fontWeight: FontWeight.bold,
                        ),
                      ),
                      const SizedBox(height: 20),
                      Text(house.description),
                      const SizedBox(height: 20),
                      const Text(
                        "Location",
                        style: TextStyle(
                          fontSize: 18,
                          fontWeight: FontWeight.bold,
                        ),
                      ),
                      const SizedBox(height: 20),
                      SizedBox(
                        height: 400,
                        width: double.infinity,
                        child: GoogleMapsView(
                          LatLng(
                            house.latitude.toDouble(),
                            house.longitude.toDouble(),
                          ),
                        ),
                      ),
                    ],
                  ),
                )
              ]),
            ),
          );
        });
  }
}

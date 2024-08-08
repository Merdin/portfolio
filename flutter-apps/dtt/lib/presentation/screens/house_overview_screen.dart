import 'package:dtt/blocs/get_houses_bloc.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:geolocator/geolocator.dart';

import '../../models/house_response.dart';
import '../../presentation/widgets/search_bar.dart';
import '../../models/house.dart';
import '../../services/geolocation_service.dart';
import '../widgets/house_overview_list_item.dart';

class HouseOverviewScreen extends StatefulWidget {
  static const identifier = "/house-overview";

  @override
  State<HouseOverviewScreen> createState() => _HouseOverviewScreenState();
}

class _HouseOverviewScreenState extends State<HouseOverviewScreen> {
  final TextEditingController _searchController = TextEditingController();
  final FocusNode _searchFocusNode = FocusNode();

  Widget listWidget(HouseResponse data) {
    List<House> houses = _searchController.text.isEmpty
        ? data.houses.toList()
        : data.filter(_searchController.text);
    houses.sortByPrice;

    return ListView.builder(
      itemBuilder: (ctx, index) {
        final house = houses[index];
        return Padding(
          padding: const EdgeInsets.symmetric(vertical: 5.0),
          child: FutureBuilder(
            future: house.distance,
            builder: (context, snapshot) => HouseOverviewListItem(
              id: house.id,
              imageUrl: house.image,
              address: "${house.zip} ${house.city}",
              bedrooms: house.bedrooms,
              size: house.size,
              distance: snapshot.data?.toStringAsFixed(2) ?? "N/A",
              price: house.price.toString(),
              bathrooms: house.bathrooms,
            ),
          ),
        );
      },
      itemCount: houses.length,
    );
  }

  Widget buildHouses(BuildContext context) {
    housesBloc.getHouses();
    return StreamBuilder<HouseResponse>(
      stream: housesBloc.subject.stream,
      builder: (context, AsyncSnapshot<HouseResponse> snapshot) {
        if (snapshot.hasData) {
          final data = snapshot.data;
          final error = snapshot.data?.error;
          if ((error != null && error.isNotEmpty) || (data == null)) {
            return Center(
                child: Column(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                Text("Error occured: $error"),
              ],
            ));
          }

          return listWidget(data);
        } else if (snapshot.hasError) {
          return Center(
              child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              Text("Error occured: ${snapshot.error}"),
            ],
          ));
        } else {
          return const Center(
            child: CircularProgressIndicator(),
          );
        }
      },
    );
  }

  @override
  void dispose() {
    _searchController.dispose();
    _searchFocusNode.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: SafeArea(
        child: Padding(
          padding: const EdgeInsets.symmetric(horizontal: 18),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              const Text("DTT REAL ESTATE"),
              const SizedBox(height: 18),
              SearchBar(
                _searchController,
                _searchFocusNode,
                onIconPressed: () => setState(() {
                  _searchFocusNode.unfocus();
                  _searchController.clear();
                }),
                onChanged: (_) {
                  setState(() {});
                },
              ),
              const SizedBox(height: 18),
              Expanded(
                child: LayoutBuilder(builder: (ctx, constraints) {
                  return buildHouses(context);
                }),
              ),
            ],
          ),
        ),
      ),
    );
  }
}

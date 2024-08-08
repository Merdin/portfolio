import 'package:flutter/material.dart';
import 'package:google_maps_flutter/google_maps_flutter.dart';

class GoogleMapsView extends StatefulWidget {
  final LatLng _center;

  const GoogleMapsView(this._center);

  @override
  State<GoogleMapsView> createState() => _GoogleMapsViewState();
}

class _GoogleMapsViewState extends State<GoogleMapsView> {
  late GoogleMapController mapController;

  void _onMapCreated(GoogleMapController controller) {
    mapController = controller;
  }

  @override
  Widget build(BuildContext context) {
    return GoogleMap(
      onMapCreated: _onMapCreated,
      myLocationButtonEnabled: false,
      initialCameraPosition: CameraPosition(
        target: widget._center,
        zoom: 13.0,
      ),
      markers: {
        Marker(
          markerId: MarkerId("${widget._center}"),
          position: widget._center,
        ),
      },
    );
  }
}

import 'dart:collection';

import 'package:dio/dio.dart';
import 'package:flutter/material.dart';
import 'package:google_maps_flutter/google_maps_flutter.dart';
import 'package:tracker/tracker/wearer_details_screen.dart';

import '../utils/httpManager.dart';
import 'package:tracker/utils/localDb.dart';

// this screen is used for display the current location of the tracker
class SingleWearerMapScreen extends StatefulWidget {
  final String name;
  final String imei;
  SingleWearerMapScreen({Key? key, required this.imei, required this.name}) : super(key: key);

  @override
  State<SingleWearerMapScreen> createState() => _SingleWearerMapScreenState();
}

class _SingleWearerMapScreenState extends State<SingleWearerMapScreen> {
  Set<Marker> _markers = HashSet<Marker>();
  late GoogleMapController _mapController;
  bool _isLoading = false;
  LatLng initLocation = LatLng(-36.848461, 174.763336);

  @override
  void initState() {
    super.initState();
    fetchLocation();
  }

  void fetchLocation() async {
    setState(() {
      _isLoading = true;
    });
    Response response = await HttpManager.get('/data/location?imei=${this.widget.imei}');
    print(response.data);
    setState(() {
      initLocation = LatLng(response.data["latitude"], response.data["longitude"]);
      _markers.add(Marker(
        markerId: MarkerId(this.widget.imei),
        position: LatLng(response.data["latitude"], response.data["longitude"]),
        infoWindow: InfoWindow(
          onTap: () {},
          title: this.widget.name,
          snippet: this.widget.imei),
      ));
      _isLoading = false;
    });
  }

  void _onMapCreated(GoogleMapController controller) {
    _mapController = controller;
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Container(
        height: MediaQuery.of(context).size.height,
        width: double.infinity,
        child: _isLoading
            ? Center(
                child: CircularProgressIndicator(),
              )
            : GoogleMap(
                onMapCreated: _onMapCreated,
                zoomControlsEnabled: false,
                initialCameraPosition: CameraPosition(
                  target: initLocation,
                  zoom: 15,
                ),
                markers: _markers,
              ),
      ),
    );
  }
}

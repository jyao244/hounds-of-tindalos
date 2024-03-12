import 'dart:async';
import 'dart:collection';

import 'package:dio/dio.dart';
import 'package:flutter/material.dart';
import 'package:google_maps_flutter/google_maps_flutter.dart';
import 'package:tracker/tracker/wearer_details_screen.dart';

import '../utils/httpManager.dart';
import 'package:tracker/utils/localDb.dart';

// this screen is used for display the current location of the tracker
class MapScreen extends StatefulWidget {
  MapScreen({Key? key}) : super(key: key);

  @override
  State<MapScreen> createState() => _MapScreenState();
}

class _MapScreenState extends State<MapScreen> {
  Set<Marker> _markers = HashSet<Marker>();
  late GoogleMapController _mapController;
  bool _isLoading = true;
  LatLng? initLocation;
  Timer? _timer;

  @override
  void initState() {
    super.initState();
    print('fetch location first time');
    fetchLocation();
    setTimer();
  }

  @override
  void dispose() {
    _timer?.cancel();
    super.dispose();
  }

  void toWearDetailsScreen(String imei) async {
    Response response = await HttpManager.get('/elderly/imei?imei=${imei}');
    var details = response.data;
    print(details);
    Navigator.push(
        context,
        MaterialPageRoute(
            builder: (context) => WearerDetailsScreen(
                  name: details['name'],
                  elderlyId: details['id'],
                  imei: imei,
                  avatar: details.containsKey('avatar')
                      ? details['avatar']
                      : 'https://stamp.fyi/avatar/${details['imei']}',
                )));
  }

  void fetchLocation() async {
    String userId = LocalDB.getString('id');
    Response response = await HttpManager.get('/elderly/all?id=$userId');
    print(response.data);
    setState(() {
      for (var tracker in response.data) {
        initLocation = LatLng(tracker["latitude"], tracker["longitude"]);
        _markers.add(Marker(
          markerId: MarkerId(tracker['_id']),
          position: LatLng(tracker["latitude"], tracker["longitude"]),
          infoWindow: InfoWindow(
              onTap: () {
                toWearDetailsScreen(tracker['imei']);
              },
              title: tracker['name'],
              snippet: tracker['imei']),
        ));
      }
      _isLoading = false;
    });
  }

  void setTimer() {
    _timer = Timer.periodic(new Duration(seconds: 15), (t) async {
      try {
        print("fetching location again");
        fetchLocation();
      } catch (e) {
        print("error");
      }
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
                  target: initLocation!,
                  zoom: 15,
                ),
                markers: _markers,
              ),
      ),
    );
  }
}

import 'dart:collection';

import 'package:dio/dio.dart';
import 'package:flutter/material.dart';
import 'package:google_maps_flutter/google_maps_flutter.dart';
import 'package:tracker/utils/colors.dart';
import 'package:tracker/utils/httpManager.dart';

class SafeZoneSettingsScreen extends StatefulWidget {
  final String imei;
  SafeZoneSettingsScreen({Key? key, required this.imei}) : super(key: key);

  @override
  State<SafeZoneSettingsScreen> createState() => _SafeZoneSettingsScreenState();
}

class _SafeZoneSettingsScreenState extends State<SafeZoneSettingsScreen> {
  Set<Polygon> _polygons = HashSet<Polygon>();
  List<Marker> _markers = <Marker>[];
  Set<Marker> _display = HashSet<Marker>();
  final TextEditingController _textFieldController = TextEditingController();

  int countOfPolygon = 0;
  int countOfMarker = 0;

  // the initial position of Auckland
  double latitude = -36.848461;
  double longitude = 174.763336;

  // controller
  late GoogleMapController _mapController;

  Future<void> _displayTextInputDialog(BuildContext context) async {
    return showDialog(
      context: context,
      builder: (context) {
        return AlertDialog(
          title: Text('TextField in Dialog'),
          content: TextField(
            onChanged: (value) {},
            controller: _textFieldController,
            decoration: InputDecoration(hintText: "Give a name to the zone"),
          ),
          actions: <Widget>[
            TextButton(
              child: Text('CANCEL'),
              onPressed: () {
                setState(() {
                  Navigator.pop(context);
                });
              },
            ),
            TextButton(
              child: Text('OK'),
              onPressed: () {
                setState(() {
                  _setPolygon();
                  Navigator.pop(context);
                });
              },
            ),
          ],
        );
      },
    );
  }

  @override
  void initState() {
    super.initState();
    fetchSafeZone();
  }

  void fetchSafeZone() async {
    Response response =
        await HttpManager.get('/safezone?imei=${this.widget.imei}');
    List<LatLng> polygonLatLongs = [];
    // todo support list
    for (var safezone in response.data) {
      for (var item in safezone["locations"]) {
        polygonLatLongs.add(new LatLng(item["latitude"], item["longitude"]));
      }
      _polygons.add(Polygon(
        polygonId: PolygonId(countOfPolygon.toString()),
        points: polygonLatLongs,
        strokeColor: primaryColor,
        strokeWidth: 3,
        fillColor: tertiaryColor,
      ));
      polygonLatLongs = [];
      countOfPolygon++;
    }
    setState(() {});
  }

  void _addMarker() {
    setState(() {
      Marker m = Marker(
        markerId: MarkerId(countOfMarker.toString()),
        position: LatLng(latitude, longitude),
        infoWindow: InfoWindow(
            title: 'This is position',
            snippet: latitude.toString() + "," + longitude.toString()),
      );
      _markers.add(m);
      _display.add(m);
      countOfMarker++;
    });
  }

  void _setPolygon() {
    setState(() {
      List<LatLng> polygonLatLongs = [];
      var locations = [];
      for (int i = 0; i < _markers.length; i++) {
        Marker tem = _markers.elementAt(i);
        locations.add({
          "longitude": tem.position.longitude,
          "latitude": tem.position.latitude
        });
        polygonLatLongs.add(tem.position);
      }
      Marker tem = _markers.elementAt(0);
      polygonLatLongs.add(tem.position);

      HttpManager.post('/safezone/add', data: {
        "imei": this.widget.imei,
        "locations": locations,
        'name': _textFieldController.text
      });

      _polygons.add(Polygon(
        polygonId: PolygonId(countOfPolygon.toString()),
        points: polygonLatLongs,
        strokeColor: primaryColor,
        strokeWidth: 3,
        fillColor: tertiaryColor,
      ));
      countOfPolygon++;
      _markers.clear();
      _display.clear();
    });
  }

  void _onMapCreated(GoogleMapController controller) {
    _mapController = controller;
  }

  @override
  Widget build(BuildContext context) {
    double screenWidth = MediaQuery.of(context).size.width - 35;
    double screenHeight = MediaQuery.of(context).size.height - 25;

    double middleX = screenWidth / 2;
    double middleY = screenHeight / 2;

    return Scaffold(
        body: Stack(children: [
      Container(
          height: MediaQuery.of(context).size.height,
          width: double.infinity,
          child: GoogleMap(
            onMapCreated: _onMapCreated,
            zoomControlsEnabled: false,
            initialCameraPosition: CameraPosition(
              target: LatLng(-36.848461, 174.763336),
              zoom: 15,
            ),
            markers: _display,
            polygons: _polygons,
            onCameraMove: (CameraPosition position) {
              setState(() {
                latitude = position.target.latitude;
                longitude = position.target.longitude;
              });
            },
          )),
      Positioned(
        right: middleX,
        bottom: middleY,
        child: Icon(
          Icons.add,
          size: 36,
          color: primaryColor,
        ),
      ),
      Positioned(
        left: 15,
        bottom: 15,
        child: FloatingActionButton(
          backgroundColor: primaryColor,
          heroTag: 'save',
          onPressed: () {
            _displayTextInputDialog(context);
          },
          child: Icon(Icons.save),
        ),
      ),
      Positioned(
        right: 15,
        bottom: 15,
        child: FloatingActionButton(
          backgroundColor: primaryColor,
          heroTag: 'add',
          onPressed: () {
            _addMarker();
          },
          child: Icon(Icons.add),
        ),
      ),
    ]));
  }
}

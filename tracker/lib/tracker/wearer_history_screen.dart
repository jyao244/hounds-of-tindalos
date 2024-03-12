import 'dart:collection';

import 'package:dio/dio.dart';
import 'package:flutter/material.dart';
import 'package:google_maps_flutter/google_maps_flutter.dart';
import 'package:tracker/utils/colors.dart';
import 'package:flutter_datetime_picker/flutter_datetime_picker.dart';

import '../utils/httpManager.dart';

import 'package:http/http.dart' as http;

// this screen is used for display the history of the tracker
class WearerHistoryScreen extends StatefulWidget {
  final String imei;
  WearerHistoryScreen({Key? key, required this.imei}) : super(key: key);

  @override
  State<WearerHistoryScreen> createState() => _WearerHistoryScreenState();
}

class _WearerHistoryScreenState extends State<WearerHistoryScreen> {
  bool _isLoading = false;
  Set<Marker> _markers = HashSet<Marker>();
  late GoogleMapController _mapController;
  late BitmapDescriptor _markerIcon;
  DateTime start = DateTime.parse('2022-01-01 01:01:01');
  DateTime end = DateTime.now().add(const Duration(hours: 24));

  LatLng _startPoint = LatLng(-36.852745, 174.770119);
  LatLng _endPoint = LatLng(-36.852745, 174.770119);

  // record the history of the tracker
  List<LatLng> _polylineCoordinates = [];

  @override
  void dispose() {
    super.dispose();
  }

  @override
  void initState() {
    super.initState();
    getPolyPoints(start, end);
  }

  void getPolyPoints(DateTime dt1, DateTime dt2) async {
    setState(() {
      _isLoading = true;
    });
    _polylineCoordinates = [];
    _markers = HashSet<Marker>();
    Response response = await HttpManager.get(
        '/data/footprint?imei=${this.widget.imei}&startTime=${dt1.toString()}Z&endTime=${dt2.toString()}Z');
    print(response);
    if (response.data.length > 0) {
      for (int i = 0; i < response.data.length; i++) {
        _polylineCoordinates.add(LatLng(
            response.data[i]['latitude'], response.data[i]['longitude']));
      }
      _startPoint =
          LatLng(response.data[0]['latitude'], response.data[0]['longitude']);
      _endPoint = LatLng(response.data[response.data.length - 1]['latitude'],
          response.data[response.data.length - 1]['longitude']);
      _markers.add(Marker(
        markerId: MarkerId('1'),
        position: _startPoint,
        infoWindow: InfoWindow(title: 'The start point'),
      ));
      _markers.add(Marker(
        markerId: MarkerId('2'),
        position: _endPoint,
        infoWindow: InfoWindow(title: 'The end point'),
      ));
    }
    setState(() {
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
        child: Stack(
          children: [
            GoogleMap(
              onMapCreated: _onMapCreated,
              zoomControlsEnabled: false,
              initialCameraPosition: CameraPosition(
                target: _endPoint,
                zoom: 15,
              ),
              markers: _markers,
              polylines: {
                Polyline(
                  polylineId: PolylineId('route'),
                  points: _polylineCoordinates,
                  color: primaryColor,
                  width: 6,
                )
              },
            ),
            Positioned(
              right: 16,
              bottom: 16,
              child: FloatingActionButton(
                backgroundColor: primaryColor,
                heroTag: 'add',
                onPressed: () => showDialog<String>(
                  context: context,
                  builder: (BuildContext context) => AlertDialog(
                    title: const Text('Choosing date'),
                    content: Text(
                        "Please choose the time period of footprint you want to see"),
                    actions: <Widget>[
                      TextButton(
                        onPressed: () => {
                          DatePicker.showDateTimePicker(context,
                              showTitleActions: true, onChanged: (date) {
                            print('change $date in time zone ' +
                                date.timeZoneOffset.inHours.toString());
                          }, onConfirm: (date) {
                            print('confirm $date');
                            start = date;
                          },
                              currentTime: DateTime(
                                  end.year, end.month, end.day, 0, 0, 0))
                        },
                        child: const Text('Start date'),
                      ),
                      TextButton(
                        onPressed: () => {
                          DatePicker.showDateTimePicker(context,
                              showTitleActions: true, onChanged: (date) {
                            print('change $date in time zone ' +
                                date.timeZoneOffset.inHours.toString());
                          }, onConfirm: (date) {
                            print('confirm $date');
                            end = date;
                          },
                              currentTime: DateTime(
                                  end.year, end.month, end.day, 0, 0, 0))
                        },
                        child: const Text('End date'),
                      ),
                      TextButton(
                        onPressed: () =>
                            {getPolyPoints(start, end), Navigator.pop(context)},
                        child: const Text('Confirm'),
                      ),
                    ],
                  ),
                ),
                child: Icon(Icons.date_range),
              ),
            ),
          ],
        ),
      ),
    );
  }
}

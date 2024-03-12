import 'package:dio/dio.dart';
import 'package:flutter/material.dart';
import 'package:tracker/settings/safe_zone_manage_screen.dart';
import 'package:tracker/settings/safe_zone_settings_screen.dart';
import 'package:tracker/settings/wearer_settings_screen.dart';
import 'package:tracker/tracker/single_wearer_map_screen.dart';
import 'package:tracker/tracker/wearer_data_collection_screen.dart';
import 'package:tracker/tracker/wearer_history_screen.dart';
import 'package:tracker/utils/colors.dart';
import 'package:tracker/utils/httpManager.dart';
import 'package:tracker/widgets/personal_info_card.dart';

import '../utils/localDb.dart';

class WearerDetailsScreen extends StatefulWidget {
  final String name;
  final String imei;
  final String elderlyId;
  final String avatar;
  WearerDetailsScreen(
      {Key? key,
      required this.name,
      required this.imei,
      required this.elderlyId,
      required this.avatar})
      : super(key: key);

  @override
  State<WearerDetailsScreen> createState() => _WearerDetailsScreenState();
}

class _WearerDetailsScreenState extends State<WearerDetailsScreen> {
  bool _isLoading = false;
  var details;
  var actualAddress;

  @override
  void initState() {
    super.initState();
    getDetails();
  }

  @override
  void dispose() {
    super.dispose();
  }

  Future<void> getActualAddress(String latitude, String longitude) async {
    // actualAddress = "123 Fake Street, Auckland";
    try {
      Response response = await HttpManager.get(
          'https://maps.googleapis.com/maps/api/geocode/json?latlng=$latitude,$longitude&key=xxx', // put your own google api key here
          prefix: false);
      actualAddress = response.data['results'][0]['formatted_address'];
    } catch (e) {
      actualAddress = 'loading...';
    }
  }

  void getDetails() async {
    setState(() {
      _isLoading = true;
    });
    // todo need to change this imei to the elder's id
    print(this.widget.imei);
    Response response =
        await HttpManager.get('/data/details?imei=${this.widget.imei}');
    details = response.data;
    await getActualAddress(
        details['latitude'].toString(), details['longitude'].toString());
    print(details);
    setState(() {
      _isLoading = false;
    });
  }

  void deleteDevice() async {
    print(details);
    setState(() {
      _isLoading = true;
    });
    Response response = await HttpManager.delete('/elderly/remove', data: {
      'elderlyId': this.widget.elderlyId,
      'userId': LocalDB.getString('id')
    });
    details = response.data;
    Navigator.pop(context);
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: whiteColor,
      body: SafeArea(
        child: _isLoading
            ? Center(
                child: CircularProgressIndicator(),
              )
            : Container(
                padding:
                    const EdgeInsets.symmetric(horizontal: 16, vertical: 32),
                width: double.infinity,
                child: SingleChildScrollView(
                  child: Wrap(
                    runSpacing: 16,
                    alignment: WrapAlignment.center,
                    children: [
                      getStatusBar(this.widget.avatar, this.widget.name,
                          "IMEI: ${this.widget.imei}"),
                      InkWell(
                        onTap: () {
                          Navigator.push(
                            context,
                            MaterialPageRoute(
                                builder: (context) => SingleWearerMapScreen(
                                      imei: this.widget.imei,
                                      name: this.widget.name,
                                    )),
                          );
                        },
                        child: DetailedListTile(
                          "Address",
                          actualAddress ?? "Loading...",
                          Icon(Icons.signpost),
                        ),
                      ),
                      InkWell(
                        onTap: () {
                          Navigator.push(
                            context,
                            MaterialPageRoute(
                                builder: (context) => SingleWearerMapScreen(
                                      imei: this.widget.imei,
                                      name: this.widget.name,
                                    )),
                          );
                        },
                        child: DetailedListTile(
                          "Location",
                          '${details['latitude']}, ${details['longitude']}',
                          Icon(Icons.pin_drop),
                        ),
                      ),
                      DetailedListTile(
                        "Wearing State",
                        '${details['wear'] ? "Wearing" : "Taken off"}',
                        Icon(Icons.account_circle),
                      ),
                      InkWell(
                        onTap: () {
                          Navigator.push(
                            context,
                            MaterialPageRoute(
                                builder: (context) =>
                                    WearerDataCollectionScreen(
                                      imei: this.widget.imei,
                                      type: "heartRate,steps",
                                      title: "Steps",
                                      name: "steps",
                                    )),
                          );
                        },
                        child: DetailedListTile(
                          "Steps",
                          '${details['steps']}',
                          Icon(Icons.directions_run),
                        ),
                      ),
                      InkWell(
                        onTap: () {
                          Navigator.push(
                            context,
                            MaterialPageRoute(
                                builder: (context) =>
                                    WearerDataCollectionScreen(
                                      imei: this.widget.imei,
                                      type: "heartRate,heartRate",
                                      title: "Heart Rate",
                                      name: "heart rate",
                                    )),
                          );
                        },
                        child: DetailedListTile(
                          "Heart Rate",
                          '${details['heartRate']}/min',
                          Icon(Icons.monitor_heart),
                        ),
                      ),
                      InkWell(
                        onTap: () {
                          Navigator.push(
                            context,
                            MaterialPageRoute(
                                builder: (context) =>
                                    WearerDataCollectionScreen(
                                      imei: this.widget.imei,
                                      type:
                                          "bloodPressure,highPressure,lowPressure",
                                      title: "Blood Pressure",
                                      name: "systolic pressure,diastolic pressure",
                                    )),
                          );
                        },
                        child: DetailedListTile(
                          "Blood Pressure",
                          '${details['lowPressure']}/${details['highPressure']}mmHg',
                          Icon(Icons.water_drop),
                        ),
                      ),
                      InkWell(
                        onTap: () {
                          Navigator.push(
                            context,
                            MaterialPageRoute(
                                builder: (context) =>
                                    WearerDataCollectionScreen(
                                      imei: this.widget.imei,
                                      type: "temperature,temp",
                                      title: "Body tempurature",
                                      name: "body tempurature",
                                    )),
                          );
                        },
                        child: DetailedListTile(
                          "Body tempurature",
                          '${details['temp']}°C',
                          Icon(Icons.device_thermostat),
                        ),
                      ),
                      InkWell(
                        onTap: () {
                          Navigator.push(
                            context,
                            MaterialPageRoute(
                                builder: (context) =>
                                    WearerDataCollectionScreen(
                                      imei: this.widget.imei,
                                      type: "bloodOxygen,bloodOxygen",
                                      title: "Blood Oxygen",
                                      name: "blood oxygen",
                                    )),
                          );
                        },
                        child: DetailedListTile(
                          "Blood Oxygen",
                          '${details['bloodOxygen']}%',
                          Icon(Icons.bloodtype),
                        ),
                      ),
                      InkWell(
                        onTap: () {
                          Navigator.push(
                            context,
                            MaterialPageRoute(
                                builder: (context) =>
                                    WearerDataCollectionScreen(
                                      imei: this.widget.imei,
                                      type: "heartRate,deepSleep,lightSleep",
                                      title: "Sleep",
                                      name: "deep sleep,light sleep",
                                    )),
                          );
                        },
                        child: DetailedListTile(
                          "Sleep",
                          'start: ${details['startSleep'].split('T')[1].split('.')[0]}\nend:${details['endSleep'].split('T')[1].split('.')[0]}\ndeep/light: ${details['deepSleep']}/${details['lightSleep']}min',
                          Icon(Icons.bed),
                        ),
                      ),
                      // update the wearer information
                      InkWell(
                        onTap: () {
                          Navigator.push(
                            context,
                            MaterialPageRoute(
                                builder: (context) => WearerSettingsScreen(
                                      elderlyId: this.widget.elderlyId,
                                      avatar: this.widget.avatar,
                                    )),
                          );
                        },
                        child: DetailedListTile(
                          "Wearer Information",
                          "Update Wearer Information",
                          Icon(Icons.settings),
                          hasMore: true,
                        ),
                      ),
                      // check the wearer history -> footprint
                      InkWell(
                        onTap: () {
                          // todo
                          Navigator.push(
                            context,
                            MaterialPageRoute(
                                builder: (context) => WearerHistoryScreen(
                                    imei: this.widget.imei)),
                          );
                        },
                        child: DetailedListTile(
                          "Footprint",
                          "Check footprint",
                          Icon(Icons.history),
                          hasMore: true,
                        ),
                      ),
                      InkWell(
                        onTap: () {
                          Navigator.push(
                            context,
                            MaterialPageRoute(
                              builder: (context) => SafeZoneSettingsScreen(
                                  imei: this.widget.imei),
                            ),
                          );
                        },
                        child: DetailedListTile(
                          "Safe Zone",
                          "Set Safe Zone",
                          Icon(Icons.shield_rounded),
                          hasMore: true,
                        ),
                      ),
                      // manage safe zone
                      InkWell(
                        onTap: () {
                          Navigator.push(
                            context,
                            MaterialPageRoute(
                              builder: (context) =>
                                  SafeZoneManageScreen(imei: this.widget.imei),
                            ),
                          );
                        },
                        child: DetailedListTile(
                          "Safe zone management",
                          "delete safe zone",
                          Icon(Icons.settings),
                          hasMore: true,
                        ),
                      ),
                      InkWell(
                        onTap: () => showDialog<String>(
                          context: context,
                          builder: (BuildContext context) => AlertDialog(
                            title: const Text('Warming'),
                            content: Text("Are you sure you want to unbind?"),
                            actions: <Widget>[
                              TextButton(
                                onPressed: () => Navigator.pop(context, 'No'),
                                child: const Text('No'),
                              ),
                              TextButton(
                                onPressed: () => {
                                  deleteDevice(),
                                  Navigator.pop(context, 'Yes'),
                                },
                                child: const Text('Yes'),
                              ),
                            ],
                          ),
                        ),
                        child: DetailedListTile(
                          "Unbind",
                          "Remove wearer and unbind from device",
                          Icon(
                            Icons.link_rounded,
                            color: Colors.red,
                          ),
                        ),
                      ),
                    ],
                  ),
                ),
              ),
      ),
    );
  }

  DetailedListTile(title, subtitle, leading, {hasMore = false}) {
    return ListTile(
        title: Text(title),
        subtitle: Text(subtitle),
        leading: leading,
        trailing: hasMore ? Icon(Icons.arrow_right) : null);
  }
}

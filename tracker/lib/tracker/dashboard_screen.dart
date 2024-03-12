import 'dart:async';

import 'package:dio/dio.dart';
import 'package:flutter/material.dart';
import 'package:tracker/tracker/add_device_screen.dart';
import 'package:tracker/tracker/wearer_details_screen.dart';
import 'package:tracker/utils/colors.dart';
import 'package:tracker/widgets/add_device_card.dart';
import 'package:tracker/widgets/device_card.dart';
import 'package:tracker/utils/httpManager.dart';
import 'package:tracker/utils/localDb.dart';

// this screen is used for display the dashboard of different trackers
class DashboardScreen extends StatefulWidget {
  DashboardScreen({Key? key}) : super(key: key);

  @override
  State<DashboardScreen> createState() => _DashboardScreenState();
}

class _DashboardScreenState extends State<DashboardScreen> {
  bool _isLoading = true;
  var trackerList = [];
  Timer? _timer;

  @override
  void initState() {
    super.initState();
    print('fetch tracker list first time');
    fetchTrackerList();
    setTimer();
  }

  @override
  void dispose() {
    _timer?.cancel();
    super.dispose();
  }

  void fetchTrackerList() async {
    String userId = LocalDB.getString('id');
    Response response = await HttpManager.get('/elderly/all?id=$userId');
    trackerList = response.data;
    if (trackerList.length == 0) {
      Navigator.push(
          context, MaterialPageRoute(builder: (context) => AddDeviceScreen()));
    }
    setState(() {
      _isLoading = false;
    });
  }

  void setTimer() {
    _timer = Timer.periodic(new Duration(seconds: 15), (t) async {
      try {
        print("fetching location again");
        fetchTrackerList();
      } catch (e) {
        print("error");
      }
    });
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
                padding: const EdgeInsets.only(left: 16, right: 16, top: 32),
                width: double.infinity,
                child: GridView.count(
                  mainAxisSpacing: 10,
                  crossAxisSpacing: 10,
                  crossAxisCount: 2,
                  children: List.generate(trackerList.length + 1, (index) {
                    if (index == trackerList.length) {
                      return AddDeviceCard();
                    } else {
                      return InkWell(
                          onTap: () => {
                                _timer?.cancel(),
                                Navigator.push(
                                  context,
                                  MaterialPageRoute(
                                    builder: (context) => WearerDetailsScreen(
                                      name:
                                          trackerList[index].containsKey('name')
                                              ? trackerList[index]['name']
                                              : 'Unknown',
                                      imei: trackerList[index]['imei'],
                                      elderlyId: trackerList[index]['_id'],
                                      avatar: trackerList[index]
                                              .containsKey('avatar')
                                          ? trackerList[index]['avatar']
                                          : 'https://stamp.fyi/avatar/${trackerList[index]['imei']}',
                                    ),
                                  ),
                                ).then((value) => {
                                      // if some thing is changed in the wearer details screen
                                      // then we need to fetch the new data
                                      fetchTrackerList(),
                                      setTimer()
                                    })
                              },
                          child: DeviceCard(
                            username: trackerList[index].containsKey('name')
                                ? trackerList[index]['name']
                                : 'Unknown',
                            imei: trackerList[index]['imei'],
                            avatar: trackerList[index].containsKey('avatar')
                                ? trackerList[index]['avatar']
                                : 'https://stamp.fyi/avatar/${trackerList[index]['imei']}',
                          ));
                    }
                  }),
                ),
              ),
      ),
    );
  }
}

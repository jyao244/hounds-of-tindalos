import 'dart:async';

import 'package:dio/dio.dart';
import 'package:flutter/material.dart';
import 'package:tracker/tracker/wearer_details_screen.dart';
import 'package:tracker/utils/colors.dart';
import 'package:tracker/utils/httpManager.dart';
import 'package:tracker/utils/localDb.dart';

// this screen is used for display all the notification
class NotificationScreen extends StatefulWidget {
  NotificationScreen({Key? key}) : super(key: key);

  @override
  State<NotificationScreen> createState() => _NotificationScreenState();
}

class _NotificationScreenState extends State<NotificationScreen> {
  bool _isLoading = true;
  var notificationList = [];
  Timer? _timer;

  @override
  void initState() {
    super.initState();
    print('fetch notification first time');
    fetchNotification();
    setTimer();
  }

  @override
  void dispose() {
    _timer?.cancel();
    super.dispose();
  }

  void fetchNotification() async {
    String userId = LocalDB.getString('id');
    Response response = await HttpManager.get('/notification/all?id=$userId');
    notificationList = response.data;
    print(notificationList);
    setState(() {
      _isLoading = false;
    });
  }

  void setTimer() {
    _timer = Timer.periodic(new Duration(seconds: 15), (t) async {
      try {
        print("fetching notification again");
        fetchNotification();
      } catch (e) {
        print("error");
      }
    });
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
                child: ListView.builder(
                  // Let the ListView know how many items it needs to build.
                  itemCount: notificationList.length,
                  // Provide a builder function. This is where the magic happens.
                  // Convert each item into a widget based on the type of item it is.
                  itemBuilder: (context, index) {
                    final item = notificationList[index];
                    // todo update the itme style
                    return ListTile(
                        onTap: () {
                          toWearDetailsScreen(item['imei']);
                        },
                        leading: CircleAvatar(
                          radius: 16,
                          backgroundImage: NetworkImage(
                              item.containsKey('avatar')
                                  ? item['avatar']
                                  : 'https://stamp.fyi/avatar/${item['imei']}'),
                          backgroundColor: whiteColor,
                        ),
                        title: Text(
                          item['title'],
                        ),
                        subtitle: Text(item['subtitle']));
                  },
                ),
              ),
      ),
    );
  }
}

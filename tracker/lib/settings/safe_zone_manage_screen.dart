import 'package:dio/dio.dart';
import 'package:flutter/material.dart';
import 'package:tracker/utils/colors.dart';
import 'package:tracker/utils/httpManager.dart';

class SafeZoneManageScreen extends StatefulWidget {
  final String imei;
  SafeZoneManageScreen({Key? key, required this.imei}) : super(key: key);

  @override
  State<SafeZoneManageScreen> createState() => _SafeZoneManageScreenState();
}

class _SafeZoneManageScreenState extends State<SafeZoneManageScreen> {
  var safeZoneList = [];
  bool _isLoading = false;

  @override
  void initState() {
    super.initState();
    getDetails();
  }

  @override
  void dispose() {
    super.dispose();
  }

  Future<void> getDetails() async {
    setState(() {
      _isLoading = true;
    });
    Response response =
        await HttpManager.get('/safezone?imei=${this.widget.imei}');
    safeZoneList = response.data;
    setState(() {
      _isLoading = false;
    });
  }

  void deleteSafeZone(String id) async {
    setState(() {
      _isLoading = true;
    });
    safeZoneList
        .removeAt(safeZoneList.indexWhere((element) => element['_id'] == id));
    Response response = await HttpManager.delete('/safezone/remove', data: {
      'id': id,
    });
    setState(() {
      _isLoading = false;
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: whiteColor,
      body: SafeArea(
        child: _isLoading
            ? Center(child: CircularProgressIndicator())
            : Container(
                color: Colors.white,
                padding: EdgeInsets.all(20.0),
                child: Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      SizedBox(
                        height: 20,
                      ),
                      Text(
                        'Manage safe zone data',
                        style: TextStyle(
                          fontSize: 30.0,
                          fontWeight: FontWeight.bold,
                        ),
                      ),
                      SizedBox(
                        height: 20,
                      ),
                      Table(
                        children: [
                          for (var item in safeZoneList)
                            TableRow(
                              children: [
                                TableCell(
                                    verticalAlignment:
                                        TableCellVerticalAlignment.middle,
                                    child: Text(
                                      item['name'] ?? "unknown",
                                      style: TextStyle(
                                          fontSize: 20.0,
                                          fontWeight: FontWeight.bold),
                                    )),
                                TableCell(
                                  verticalAlignment:
                                      TableCellVerticalAlignment.middle,
                                  child: TextButton(
                                    onPressed: () {
                                      deleteSafeZone(item['_id']);
                                    },
                                    child: Text(
                                      'Delete',
                                      style: TextStyle(fontSize: 20.0),
                                    ),
                                  ),
                                )
                              ],
                            ),
                        ],
                      ),
                    ]),
              ),
      ),
    );
  }
}

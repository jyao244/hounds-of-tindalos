import 'package:dio/dio.dart';
import 'package:flutter/material.dart';
import 'package:tracker/utils/colors.dart';
import 'package:tracker/utils/httpManager.dart';
import 'package:tracker/utils/localDb.dart';

class WearerManageScreen extends StatefulWidget {
  WearerManageScreen({Key? key}) : super(key: key);

  @override
  State<WearerManageScreen> createState() => _WearerManageScreenState();
}

class _WearerManageScreenState extends State<WearerManageScreen> {
  var WearerList = [];
  bool _isLoading = false;

  @override
  void initState() {
    super.initState();
    getDetails();
  }

  Future<void> getDetails() async {
    setState(() {
      _isLoading = true;
    });
    String userId = LocalDB.getString('id');
    Response response = await HttpManager.get('/elderly/all?id=$userId');
    WearerList = response.data;
    setState(() {
      _isLoading = false;
    });
  }

  void deleteWearer(String id) async {
    setState(() {
      _isLoading = true;
    });
    Response response = await HttpManager.delete('/elderly/remove',
        data: {'elderlyId': id, 'userId': LocalDB.getString('id')});
    getDetails();
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
                      'Manage device',
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
                        for (var item in WearerList)
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
                                    deleteWearer(item['_id']);
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
                  ],
                ),
              ),
      ),
    );
  }
}

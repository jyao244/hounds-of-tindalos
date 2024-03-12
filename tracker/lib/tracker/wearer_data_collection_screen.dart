import 'package:dio/dio.dart';
import 'package:flutter/material.dart';
import 'package:flutter_datetime_picker/flutter_datetime_picker.dart';
import 'package:syncfusion_flutter_charts/charts.dart';
import 'package:syncfusion_flutter_charts/sparkcharts.dart';

import '../utils/colors.dart';
import '../utils/httpManager.dart';

class WearerDataCollectionScreen extends StatefulWidget {
  final String title;
  final String type;
  final String imei;
  final String name;
  WearerDataCollectionScreen({
    Key? key, 
    required this.imei, 
    required this.type, 
    required this.title,
    required this.name,}) 
    : super(key: key);

  @override
  State<WearerDataCollectionScreen> createState() => _WearerDataCollectionScreenState();
}

class _WearerDataCollectionScreenState extends State<WearerDataCollectionScreen> {
  bool _isLoading = false;
  DateTime start = DateTime.parse('2022-01-01 01:01:01');
  DateTime end = DateTime.now().add(const Duration(hours: 24));
  late List<List<SalesData>> chartData = [];

  @override
  void initState() {
    getData(start, end);
    super.initState();
  }

  @override
  void dispose() {
    super.dispose();
  }

  void getData(DateTime dt1, DateTime dt2) async {
    setState(() {
      _isLoading = true;
    });
    Response response = await HttpManager.get(
      '/data/biology?imei=${this.widget.imei}&startTime=${dt1.toString()}Z&endTime=${dt2.toString()}Z');
    print(response);
    var types = this.widget.type.split(',');
    var data=response.data[types[0]];
    List<SalesData> temp;
    for(int i=1;i<types.length;i++){
      temp = [];
      for(int j=0;j<data.length;j++){
        temp.add(SalesData(DateTime.parse(data[j]['date']), data[j][types[i]].toDouble()));
        print(DateTime.parse(data[j]['date']));
        print(data[j][types[i]].toDouble());
      }
      chartData.add(temp);
    }
    setState(() {
      _isLoading = false;
    });
  }

  List<List<SalesData>> testchartData = [[
            SalesData(DateTime.parse('2022-01-01 01:01:01'), 35),
            SalesData(DateTime.parse('2022-01-02 01:01:01'), 28),],
            [SalesData(DateTime.parse('2022-01-01 01:01:01'), 15),
      SalesData(DateTime.parse('2022-01-02 01:01:01'), 18),]
        ];

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Container(
        height: MediaQuery.of(context).size.height,
        width: double.infinity,
        child: Stack(children: [
          Column(children: [
            SizedBox(height: 50,),
            Text(this.widget.title, style: TextStyle(fontSize: 24)),
            SizedBox(height: 25,),
            Container(height: 500, child: SfCartesianChart(
              legend: Legend(isVisible: true, position: LegendPosition.bottom),
              primaryXAxis: DateTimeAxis(),
              // Renders line chart
              series: List<ChartSeries>.generate(chartData.length, (index) => 
                LineSeries<SalesData, DateTime>(
                  name: this.widget.name.split(',')[index],
                  dataSource: chartData[index], 
                  xValueMapper: (SalesData sales, _) => sales.year,
                  yValueMapper: (SalesData sales, _) => sales.sales))
            )),]),
          Positioned(
            top: 20,
            right: 20,
            child: FloatingActionButton(
              backgroundColor: primaryColor,
              child: Icon(Icons.date_range),
              onPressed: () => showDialog<String>(
                context: context,
                builder: (BuildContext context) => AlertDialog(
                  title: const Text('Choosing date'),
                  content: Text(
                      "Please choose the time period of physiological data you want to see"),
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
                      onPressed: () =>{getData(start, end), Navigator.pop(context)},
                      child: const Text('Confirm'),
                    ),
                  ],
                ),
              ),
            ),
          ),
          ]),
      ),
    );
  }
}

class SalesData {
  SalesData(this.year, this.sales);
  final DateTime year;
  final double sales;
}
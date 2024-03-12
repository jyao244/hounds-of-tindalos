import 'package:flutter/material.dart';
import 'package:tracker/tracker/add_device_screen.dart';
import 'package:tracker/utils/colors.dart';

class AddDeviceCard extends StatelessWidget {
  const AddDeviceCard({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return InkWell(
      onTap: () => {
        Navigator.push(
          context,
          MaterialPageRoute(builder: (context) => AddDeviceScreen()),
        )
      },
      child: Container(
        padding: const EdgeInsets.only(left: 8, right: 8, top: 8),
        width: double.infinity,
        height: 200,
        child: Container(
          child: Center(
            child: Icon(Icons.add_circle, color: whiteColor, size: 64.0),
          ),
        ),
        decoration: BoxDecoration(
          color: tertiaryColor,
          borderRadius: BorderRadius.all(Radius.circular(10)),
        ),
      ),
    );
  }
}

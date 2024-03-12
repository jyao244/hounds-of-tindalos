import 'package:flutter/material.dart';
import 'package:tracker/utils/colors.dart';

class DeviceCard extends StatelessWidget {
  final String username;
  final String imei;
  final String avatar;
  const DeviceCard(
      {Key? key,
      required this.username,
      required this.imei,
      required this.avatar})
      : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Container(
      padding: const EdgeInsets.only(left: 8, right: 8, top: 8),
      width: double.infinity,
      height: 200,
      child: Stack(children: [
        Column(
          children: [
            Text(
              username,
              style: TextStyle(
                  fontSize: 20,
                  fontWeight: FontWeight.bold,
                  color: Colors.white),
            ),
            SizedBox(
              height: 8,
            ),
            Text(
              imei,
              style: TextStyle(
                  fontSize: 14,
                  fontWeight: FontWeight.bold,
                  color: Colors.white),
            )
          ],
          mainAxisAlignment: MainAxisAlignment.start,
          crossAxisAlignment: CrossAxisAlignment.start,
        ),
        Positioned(
          bottom: 16,
          right: 8,
          child: CircleAvatar(
            radius: 16,
            backgroundImage: NetworkImage(avatar),
            backgroundColor: whiteColor,
          ),
        )
      ]),
      decoration: BoxDecoration(
        color: secondaryColor,
        borderRadius: BorderRadius.all(Radius.circular(10)),
      ),
    );
  }
}

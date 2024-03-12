import 'package:flutter/material.dart';
import 'package:tracker/utils/colors.dart';

Widget getStatusBar(String avatar, String name, String info) {
  return Container(
    padding: EdgeInsets.symmetric(horizontal: 20),
    child: Row(
      children: [
        // avatar
        CircleAvatar(
          radius: 36,
          backgroundImage: NetworkImage(avatar),
          backgroundColor: whiteColor,
        ),
        SizedBox(
          width: 16,
        ),
        Expanded(
          flex: 1,
          child: Column(
            children: [
              // name
              Text(
                name,
                style: TextStyle(
                  fontWeight: FontWeight.w600,
                  fontSize: 18,
                ),
              ),
              SizedBox(
                height: 16,
              ),
              // email address or IMEI code
              Text(info),
            ],
            crossAxisAlignment: CrossAxisAlignment.start,
          ),
        ),
      ],
    ),
  );
}

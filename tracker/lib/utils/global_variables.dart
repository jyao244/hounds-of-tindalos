import 'package:flutter/material.dart';
import 'package:tracker/settings/locus_settings_screen.dart';
import 'package:tracker/tracker/dashboard_screen.dart';
import 'package:tracker/tracker/map_screen.dart';
import 'package:tracker/notification/notification_screen.dart';

List<Widget> homeScreenItems = [
  DashboardScreen(),
  MapScreen(),
  NotificationScreen(),
  LocusSettingsScreen(),
];

const API_ENDPOINT = "http://10.0.2.2:3001/api/"; //this one is for emulator
// const API_ENDPOINT = "http://localhost:3001/api/";

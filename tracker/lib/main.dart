import 'dart:async';

import 'package:firebase_core/firebase_core.dart';
import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:tracker/account/login_screen.dart';
import 'package:tracker/utils/localDb.dart';
import 'package:tracker/pages/home_screen.dart';

void main() async {
  WidgetsFlutterBinding.ensureInitialized();
  if (kIsWeb) {
    await Firebase.initializeApp(
      options: const FirebaseOptions(
        apiKey: "xxx", // put your own google api key here
        appId: "1:204364480929:web:d4f16b405d9227da030be0",
        messagingSenderId: "204364480929",
        projectId: "p4p-project-6b843",
        storageBucket: "p4p-project-6b843.appspot.com",
      ),
    );
  } else {
    await Firebase.initializeApp();
  }
  runApp(const MyApp());
}

class MyApp extends StatefulWidget {
  const MyApp({Key? key}) : super(key: key);

  @override
  State<MyApp> createState() => _MyAppState();
}

class _MyAppState extends State<MyApp> {
  // This widget is the root of your application.
  bool _isLoading = true;
  bool _isLoggedIn = false;

  @override
  void initState() {
    super.initState();
    checkLoginState();
  }

  void checkLoginState() async {
    setState(() {
      _isLoading = true;
    });
    // init the local db once, then other operation
    // do not need to wait for the db to be ready
    await LocalDB.getInstance();
    bool isLoggedIn = LocalDB.getBool("isLoggedIn");
    setState(() {
      _isLoggedIn = isLoggedIn;
      _isLoading = false;
    });
  }

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Tracker Demo',
      // set the default font family to 'SFCompactRounded'
      theme: ThemeData(fontFamily: 'SFCompactRounded'),
      debugShowCheckedModeBanner: false,
      home: _isLoading
          ? Container(
              child: Center(child: Text("Loading")),
            )
          : _isLoggedIn
              ? HomeScreen()
              : LoginScreen(),
    );
  }
}

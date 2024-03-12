import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:tracker/account/login_screen.dart';
import 'package:tracker/settings/wearer_manage_screen.dart';
import 'package:tracker/utils/localDb.dart';
import 'package:tracker/settings/account_settings_screen.dart';
import 'package:tracker/settings/password_settings_screen.dart';
import 'package:tracker/utils/colors.dart';
import 'package:tracker/widgets/personal_info_card.dart';

class LocusSettingsScreen extends StatefulWidget {
  const LocusSettingsScreen({Key? key}) : super(key: key);

  @override
  State<LocusSettingsScreen> createState() => _LocusSettingsScreenState();
}

class _LocusSettingsScreenState extends State<LocusSettingsScreen> {
  String username = "";
  String email = "";
  String avatar = '';

  @override
  void initState() {
    // TODO: implement initState
    super.initState();
    getUserInfo();
  }

  void getUserInfo() {
    setState(() {
      avatar = LocalDB.getString("avatar");
      username = LocalDB.getString("username");
      email = "Email: ${LocalDB.getString("email")}";
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
        backgroundColor: whiteColor,
        body: SafeArea(
          child: Container(
            padding: const EdgeInsets.only(left: 16, right: 16, top: 32),
            width: double.infinity,
            child: SingleChildScrollView(
              child: Wrap(
                runSpacing: 16,
                alignment: WrapAlignment.center,
                children: [
                  // User card
                  getStatusBar(
                    avatar,
                    username,
                    email,
                  ),
                  // setting items
                  ListTile(
                    title: Text('Personal Data'),
                    leading: Icon(Icons.account_circle),
                    trailing: Icon(Icons.arrow_right),
                    onTap: () {
                      Navigator.push(
                        context,
                        MaterialPageRoute(
                            builder: (context) => AccountSettingsScreen()),
                      ).then((value) => getUserInfo());
                    },
                  ),
                  ListTile(
                    title: Text('Device Management'),
                    leading: Icon(Icons.add_circle_outline),
                    trailing: Icon(Icons.arrow_right),
                    onTap: () {
                      Navigator.push(
                        context,
                        MaterialPageRoute(
                            builder: (context) => WearerManageScreen()),
                      );
                    },
                  ),
                  ListTile(
                    title: Text('Change Password'),
                    leading: Icon(Icons.lock_outline),
                    trailing: Icon(Icons.arrow_right),
                    onTap: () {
                      Navigator.push(
                        context,
                        MaterialPageRoute(
                            builder: (context) => PasswordSettingsScreen()),
                      );
                    },
                  ),
                  ListTile(
                    title: Text(
                      'Sign Out',
                      style: TextStyle(color: Colors.red),
                    ),
                    leading: Icon(
                      Icons.exit_to_app_rounded,
                      color: Colors.red,
                    ),
                    onTap: () {
                      // todo remove the stored token from the local db
                      LocalDB.setBool("isLoggedIn", false);
                      Navigator.push(
                        context,
                        MaterialPageRoute(builder: (context) => LoginScreen()),
                      );
                    },
                  ),
                ],
              ),
            ),
          ),
        ));
  }
}

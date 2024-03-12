import 'package:dio/dio.dart';
import 'package:flutter/material.dart';
import 'package:tracker/utils/httpManager.dart';
import 'package:tracker/utils/localDb.dart';
import 'package:tracker/widgets/text_field_input.dart';

import '../utils/colors.dart';

class PasswordSettingsScreen extends StatefulWidget {
  @override
  State<PasswordSettingsScreen> createState() => _PasswordSettingsScreenState();
}

class _PasswordSettingsScreenState extends State<PasswordSettingsScreen> {
  final TextEditingController _passwordController1 = TextEditingController();
  final TextEditingController _passwordController2 = TextEditingController();
  final TextEditingController _passwordController3 = TextEditingController();
  bool _isLoading = false;

  @override
  void dispose() {
    super.dispose();
    _passwordController1.dispose();
    _passwordController2.dispose();
    _passwordController3.dispose();
  }

  void changePassword() async {
    if(_passwordController2.text==_passwordController3.text){
      setState(() {
        _isLoading = true;
      });
      String id = LocalDB.getString("id");
      Response response = await HttpManager.post('/user/password?id=${id}', data: {
        "password": _passwordController2.text,
      });
      setState(() {
        _isLoading = false;
      });
      Navigator.pop(context);
    }
  }

  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: whiteColor,
      appBar: AppBar(
        backgroundColor: whiteColor,
        elevation: 0,
        leading: GestureDetector(
          child: Icon(
            Icons.arrow_back,
            color: Colors.black,
          ),
          onTap: () {
            Navigator.pop(context);
          },
        ),
      ),
      body: SafeArea(
        child: Container(
          padding: const EdgeInsets.only(left: 32, right: 32, top: 64),
          width: double.infinity,
          child: SingleChildScrollView(
            child: Wrap(
              runSpacing: 24,
              alignment: WrapAlignment.center,
              children: [
                const Text(
                  'Change Password',
                  style: TextStyle(fontWeight: FontWeight.w600, fontSize: 24),
                ),
                // text field input for your old password
                TextFieldInput(
                  textEditingController: _passwordController1,
                  hintText: "Old password",
                  textInputType: TextInputType.text,
                  isPass: true,
                ),
                // text field input for your new password
                TextFieldInput(
                  textEditingController: _passwordController2,
                  hintText: "New password",
                  textInputType: TextInputType.text,
                  isPass: true,
                ),
                // input the new password again
                TextFieldInput(
                  textEditingController: _passwordController3,
                  hintText: "Confirm new password",
                  textInputType: TextInputType.text,
                  isPass: true,
                ),
                // button Init
                InkWell(
                  onTap: changePassword,
                  child: Container(
                    child: _isLoading
                        ? const Center(
                            child: CircularProgressIndicator(
                              color: whiteColor,
                            ),
                          )
                        : const Text("Change Password",
                            style: TextStyle(color: whiteColor)),
                    width: double.infinity,
                    alignment: Alignment.center,
                    padding: const EdgeInsets.symmetric(vertical: 12),
                    decoration: const ShapeDecoration(
                      shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.all(
                          Radius.circular(4),
                        ),
                      ),
                      color: primaryColor,
                    ),
                  ),
                ),
                Container(
                  child: const Text(
                    'Enter your new password, then select Change Password.',
                  ),
                  padding: const EdgeInsets.symmetric(vertical: 12),
                ),
              ],
            ),
          ),
        ),
      ),
    );
  }
}

import 'package:dio/dio.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/svg.dart';
import 'package:tracker/pages/home_screen.dart';
import 'package:tracker/tracker/add_device_screen.dart';
import 'package:tracker/account/sign_up_screen.dart';
import 'package:tracker/utils/localDb.dart';
import 'package:tracker/widgets/text_field_input.dart';

import '../utils/colors.dart';
import '../utils/httpManager.dart';

class LoginScreen extends StatefulWidget {
  @override
  State<LoginScreen> createState() => _LoginScreenState();
}

class _LoginScreenState extends State<LoginScreen> {
  final TextEditingController _emailController = TextEditingController();
  final TextEditingController _passwordController = TextEditingController();
  bool _isLoading = false;

  @override
  void dispose() {
    super.dispose();
    _emailController.dispose();
    _passwordController.dispose();
  }

  void userLogin() async {
    setState(() {
      _isLoading = true;
    });
    Response response = await HttpManager.post('/auth/login', data: {
      "email": _emailController.text,
      "password": _passwordController.text,
    });
    print("login success: ${response.data['token']}");
    LocalDB.setString("avatar", response.data["avatar"]);
    LocalDB.setString("username", response.data["username"]);
    LocalDB.setString("email", response.data["email"]);
    LocalDB.setString("id", response.data["id"]);
    LocalDB.setString("token", response.data["token"]);
    LocalDB.setBool("isLoggedIn", true);
    setState(() {
      _isLoading = false;
    });
    // check the connect device number
    // if not device, go to add device screen
    Navigator.push(
      context,
      MaterialPageRoute(
        builder: (context) =>
            response.data['elderlys'] == 0 ? AddDeviceScreen() : HomeScreen(),
      ),
    );
  }

  Widget build(BuildContext context) {
    return Scaffold(
      body: SafeArea(
        child: Container(
          padding: const EdgeInsets.symmetric(horizontal: 32),
          width: double.infinity,
          child: Center(
            child: SingleChildScrollView(
              child: Wrap(
                runSpacing: 24,
                alignment: WrapAlignment.center,
                children: [
                  // svg image
                  // todo Warning: Flutter SVG only supports the following formats for `width` and `height`
                  // need a better solution to support other formats
                  SvgPicture.asset(
                    "assets/images/locus.svg",
                    color: Colors.black,
                    height: 96,
                  ),
                  const SizedBox(
                    height: 12,
                  ),
                  // text field input for email
                  TextFieldInput(
                    textEditingController: _emailController,
                    hintText: "Enter your email",
                    textInputType: TextInputType.emailAddress,
                  ),
                  // text field input for password
                  TextFieldInput(
                    textEditingController: _passwordController,
                    hintText: "Enter your password",
                    textInputType: TextInputType.text,
                    isPass: true,
                  ),
                  // button login
                  InkWell(
                    onTap: userLogin,
                    child: Container(
                      child: _isLoading
                          ? const Center(
                              child: CircularProgressIndicator(
                                color: whiteColor,
                              ),
                            )
                          : const Text("Log in",
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
                  // transitioning to signing up
                  Row(
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: [
                      Container(
                        child: const Text(
                          'Dont have an account?',
                        ),
                        padding: const EdgeInsets.symmetric(vertical: 12),
                      ),
                      GestureDetector(
                        onTap: () => {
                          Navigator.push(
                              context,
                              MaterialPageRoute(
                                  builder: (context) => SignupScreen()))
                        },
                        child: Container(
                          child: const Text(
                            ' Signup.',
                            style: TextStyle(
                              fontWeight: FontWeight.bold,
                            ),
                          ),
                          padding: const EdgeInsets.symmetric(vertical: 12),
                        ),
                      ),
                    ],
                  ),
                ],
              ),
            ),
          ),
        ),
      ),
    );
  }
}

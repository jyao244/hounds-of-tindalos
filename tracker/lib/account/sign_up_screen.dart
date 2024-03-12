import 'dart:typed_data';

import 'package:dio/dio.dart';
import 'package:flutter/material.dart';
import 'package:tracker/account/login_screen.dart';
import 'package:image_picker/image_picker.dart';
import 'package:tracker/tracker/add_device_screen.dart';
import 'package:tracker/utils/httpManager.dart';
import 'package:tracker/utils/localDb.dart';
import 'package:tracker/utils/colors.dart';
import '../resource/storage_methods.dart';
import '../utils/utils.dart';
import '../widgets/text_field_input.dart';

class SignupScreen extends StatefulWidget {
  @override
  State<SignupScreen> createState() => _SignupScreenState();
}

class _SignupScreenState extends State<SignupScreen> {
  final TextEditingController _emailController = TextEditingController();
  final TextEditingController _usernameController = TextEditingController();
  final TextEditingController _passwordController = TextEditingController();

  Uint8List? _image;

  bool _isLoading = false;

  late Dio dio;

  @override
  initState() {
    super.initState();
    dio = Dio();
  }

  @override
  void dispose() {
    super.dispose();
    _emailController.dispose();
    _passwordController.dispose();
    _usernameController.dispose();
  }

  void selectImage() async {
    Uint8List im = await pickImage(ImageSource.gallery);
    setState(() {
      _image = im;
    });
  }

  void userSignUp() async {
    setState(() {
      _isLoading = true;
    });
    var link = await StorageMethods().uploadImageToStorage(_image!);
    print(link);
    Response response = await HttpManager.post('/auth/register', data: {
      "username": _usernameController.text,
      "password": _passwordController.text,
      "email": _emailController.text,
      "avatar": link,
    });
    print("sign up success: ${response.data['token']}");
    LocalDB.setString("avatar", link);
    LocalDB.setString("username", _usernameController.text);
    LocalDB.setString("email", _emailController.text);
    LocalDB.setString("id", response.data["id"]);
    LocalDB.setString("token", response.data["token"]);
    LocalDB.setBool("isLoggedIn", true);
    setState(() {
      _isLoading = false;
    });
    // new register user need to connect a new device to continue
    Navigator.push(
        context, MaterialPageRoute(builder: (context) => AddDeviceScreen()));
  }

  void navigateToLogin() {
    Navigator.of(context).push(
      MaterialPageRoute(
        builder: (context) => LoginScreen(),
      ),
    );
  }

  @override
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
                  // circular widget to accept and show our selected file
                  Stack(
                    children: [
                      _image != null
                          ? CircleAvatar(
                              radius: 64,
                              backgroundImage: MemoryImage(_image!),
                            )
                          : const CircleAvatar(
                              radius: 64,
                              backgroundImage: NetworkImage(
                                  "https://images.unsplash.com/photo-1570295999919-56ceb5ecca61?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB12MHxwaG90by1wYWdlfHx12fGVufDB12fHx12&auto=format&fit=crop&w=120&q=120"),
                              backgroundColor: whiteColor,
                            ),
                      Positioned(
                        bottom: -10,
                        right: -10,
                        child: IconButton(
                          onPressed: () {
                            selectImage();
                          },
                          icon: const Icon(Icons.add_a_photo),
                        ),
                      )
                    ],
                  ),

                  // text field input for username
                  TextFieldInput(
                    textEditingController: _usernameController,
                    hintText: "Enter your username",
                    textInputType: TextInputType.text,
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

                  // button SignUp
                  InkWell(
                    onTap: userSignUp,
                    child: Container(
                      child: _isLoading
                          ? const Center(
                              child: CircularProgressIndicator(
                                color: whiteColor,
                              ),
                            )
                          : const Text("Sign up",
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
                          'Already have an account?',
                        ),
                        padding: const EdgeInsets.symmetric(vertical: 12),
                      ),
                      GestureDetector(
                        onTap: navigateToLogin,
                        child: Container(
                          child: const Text(
                            ' Login.',
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

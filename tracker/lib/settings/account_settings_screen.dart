import 'dart:typed_data';

import 'package:dio/dio.dart';
import 'package:flutter/material.dart';
import 'package:image_picker/image_picker.dart';
import 'package:tracker/resource/storage_methods.dart';
import 'package:tracker/utils/colors.dart';
import 'package:tracker/utils/httpManager.dart';
import 'package:tracker/utils/localDb.dart';
import '../utils/utils.dart';
import '../widgets/text_field_input.dart';

class AccountSettingsScreen extends StatefulWidget {
  @override
  State<AccountSettingsScreen> createState() => _AccountSettingsScreenState();
}

class _AccountSettingsScreenState extends State<AccountSettingsScreen> {
  final TextEditingController _emailController = TextEditingController();
  final TextEditingController _usernameController = TextEditingController();
  String avatar = '';

  Uint8List? _image;

  bool _isLoading = false;

  @override
  void initState() {
    super.initState();
    avatar = LocalDB.getString('avatar');
    _emailController.text = LocalDB.getString('email');
    _usernameController.text = LocalDB.getString('username');
  }

  @override
  void dispose() {
    super.dispose();
    _emailController.dispose();
    _usernameController.dispose();
  }

  void selectImage() async {
    try {
      Uint8List im = await pickImage(ImageSource.gallery);
      setState(() {
        _image = im;
      });
    } catch (e) {
      print(e);
    }
  }

  void userInfoUpdate() async {
    setState(() {
      _isLoading = true;
    });
    String id = LocalDB.getString("id");
    if (_image != null) {
      avatar = await StorageMethods().uploadImageToStorage(_image!);
    }
    Response response = await HttpManager.post('/user/update?id=${id}', data: {
      'avatar': avatar,
      "username": _usernameController.text,
      "email": _emailController.text,
    });
    LocalDB.setString('avatar', avatar);
    LocalDB.setString("username", _usernameController.text);
    LocalDB.setString("email", _emailController.text);
    setState(() {
      _isLoading = false;
    });
    Navigator.pop(context, true);
  }

  @override
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
                // circular widget to accept and show our selected file
                Stack(
                  children: [
                    _image != null
                        ? CircleAvatar(
                            radius: 64,
                            backgroundImage: MemoryImage(_image!),
                          )
                        : CircleAvatar(
                            radius: 64,
                            backgroundImage:
                                NetworkImage(avatar), // the old avatar
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
                  hintText: "Enter your new username",
                  textInputType: TextInputType.text,
                ),

                // text field input for email
                TextFieldInput(
                  textEditingController: _emailController,
                  hintText: "Enter your new email address",
                  textInputType: TextInputType.emailAddress,
                ),

                // button SignUp
                InkWell(
                  onTap: userInfoUpdate,
                  child: Container(
                    child: _isLoading
                        ? const Center(
                            child: CircularProgressIndicator(
                              color: whiteColor,
                            ),
                          )
                        : const Text("Save",
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
                    'Once you\'ve updated the information, you can click save.',
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

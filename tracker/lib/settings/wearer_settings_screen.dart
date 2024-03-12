import 'dart:typed_data';

import 'package:dio/dio.dart';
import 'package:flutter/material.dart';
import 'package:tracker/account/login_screen.dart';
import 'package:image_picker/image_picker.dart';
import 'package:tracker/utils/colors.dart';
import 'package:tracker/utils/httpManager.dart';
import 'package:tracker/utils/utils.dart';
import 'package:tracker/widgets/text_field_input.dart';

import '../resource/storage_methods.dart';

class WearerSettingsScreen extends StatefulWidget {
  final String elderlyId;
  final String avatar;
  WearerSettingsScreen(
      {Key? key, required this.elderlyId, required this.avatar})
      : super(key: key);
  @override
  State<WearerSettingsScreen> createState() => _WearerSettingsScreenState();
}

class _WearerSettingsScreenState extends State<WearerSettingsScreen> {
  final TextEditingController _genderController = TextEditingController();
  final TextEditingController _nameController = TextEditingController();
  final TextEditingController _heightController = TextEditingController();
  final TextEditingController _weightController = TextEditingController();
  final TextEditingController _birthdayController = TextEditingController();

  Uint8List? _image;

  bool _isLoading = false;

  @override
  void dispose() {
    super.dispose();
    _genderController.dispose();
    _nameController.dispose();
    _heightController.dispose();
    _weightController.dispose();
    _birthdayController.dispose();
  }

  void elderlyInfoUpdate() async {
    setState(() {
      _isLoading = true;
    });
    var avatar;
    if (_image != null) {
      avatar = await StorageMethods().uploadImageToStorage(_image!);
    } else {
      avatar = this.widget.avatar;
    }
    Response response = await HttpManager.post(
        '/elderly/update?id=${this.widget.elderlyId}',
        data: {
          "avatar": avatar,
          "name": _nameController.text,
          "gender": _genderController.text,
          "birthday": _birthdayController.text,
          "height": _heightController.text,
          "weight": _weightController.text,
        });
    setState(() {
      _isLoading = false;
    });
    Navigator.pop(context);
  }

  void selectImage() async {
    Uint8List im = await pickImage(ImageSource.gallery);
    setState(() {
      _image = im;
    });
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
                            backgroundImage: NetworkImage(
                                this.widget.avatar), // the old avatar
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

                // text field input for name
                TextFieldInput(
                  textEditingController: _nameController,
                  hintText: "Enter wearer's name",
                  textInputType: TextInputType.text,
                ),

                // text field input for contact number
                TextFieldInput(
                  textEditingController: _genderController,
                  hintText: "Enter wearer's gender",
                  textInputType: TextInputType.text,
                ),

                // text field input for address
                TextFieldInput(
                  textEditingController: _heightController,
                  hintText: "Enter wearer's height (cm)",
                  textInputType: TextInputType.text,
                ),

                // text field input for address
                TextFieldInput(
                  textEditingController: _weightController,
                  hintText: "Enter wearer's weight (kg)",
                  textInputType: TextInputType.text,
                ),

                // text field input for birth date
                // todo add date picker
                TextFieldInput(
                  textEditingController: _birthdayController,
                  hintText: "Enter wearer's birth date",
                  textInputType: TextInputType.datetime,
                ),

                InkWell(
                  onTap: elderlyInfoUpdate,
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

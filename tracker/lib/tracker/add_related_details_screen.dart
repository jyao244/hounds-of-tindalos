import 'dart:typed_data';

import 'package:dio/dio.dart';
import 'package:flutter/material.dart';
import 'package:image_picker/image_picker.dart';
import 'package:tracker/pages/home_screen.dart';
import 'package:tracker/resource/storage_methods.dart';
import 'package:tracker/utils/utils.dart';
import 'package:tracker/widgets/text_field_input.dart';

import '../utils/colors.dart';
import '../utils/httpManager.dart';
import '../utils/localDb.dart';

class AddRelatedDetailsScreen extends StatefulWidget {
  final String elderlyId;
  AddRelatedDetailsScreen({Key? key, required this.elderlyId})
      : super(key: key);
  @override
  State<AddRelatedDetailsScreen> createState() =>
      _AddRelatedDetailsScreenState();
}

class _AddRelatedDetailsScreenState extends State<AddRelatedDetailsScreen> {
  final TextEditingController _nameController = TextEditingController();
  final TextEditingController _genderController = TextEditingController();
  final TextEditingController _birthdayController = TextEditingController();
  final TextEditingController _heightController = TextEditingController();
  final TextEditingController _weightController = TextEditingController();
  Uint8List? _image;
  bool _isLoading = false;

  @override
  void dispose() {
    super.dispose();
    _nameController.dispose();
    _genderController.dispose();
    _birthdayController.dispose();
    _weightController.dispose();
    _heightController.dispose();
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

  void addDevice() async {
    setState(() {
      _isLoading = true;
    });
    // todo update logic
    var avatar;
    if (_image != null) {
      avatar = await StorageMethods().uploadImageToStorage(_image!);
    } else {
      avatar = 'https://stamp.fyi/avatar/${this.widget.elderlyId}';
    }
    Response response = await HttpManager.post(
        '/elderly/update?id=${this.widget.elderlyId}',
        data: {
          'avatar': avatar,
          "name": _nameController.text,
          "gender": _genderController.text,
          "birthday": _birthdayController.text,
          "height": _heightController.text,
          "weight": _weightController.text,
        });
    print(response.toString());
    setState(() {
      _isLoading = false;
    });
    Navigator.push(
      context,
      MaterialPageRoute(builder: (context) => HomeScreen()),
    );
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
          padding: const EdgeInsets.symmetric(horizontal: 32),
          width: double.infinity,
          child: Center(
            child: SingleChildScrollView(
              child: Wrap(
                runSpacing: 24,
                alignment: WrapAlignment.center,
                children: [
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
                  // text field input for name
                  TextFieldInput(
                    textEditingController: _nameController,
                    hintText: "wearer's name",
                    textInputType: TextInputType.number,
                  ),
                  // text field input for gender
                  TextFieldInput(
                    textEditingController: _genderController,
                    hintText: "wearer's gender",
                    textInputType: TextInputType.number,
                  ),
                  // text field input for birthday
                  TextFieldInput(
                    textEditingController: _birthdayController,
                    hintText: "wearer's birthday",
                    textInputType: TextInputType.number,
                  ),
                  // text field input for height
                  TextFieldInput(
                    textEditingController: _heightController,
                    hintText: "wearer's height",
                    textInputType: TextInputType.number,
                  ),
                  // text field input for weight
                  TextFieldInput(
                    textEditingController: _weightController,
                    hintText: "wearer's weight",
                    textInputType: TextInputType.number,
                  ),
                  // button Init
                  InkWell(
                    onTap: addDevice,
                    child: Container(
                      child: _isLoading
                          ? const Center(
                              child: CircularProgressIndicator(
                                color: whiteColor,
                              ),
                            )
                          : const Text("Confirm",
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
                ],
              ),
            ),
          ),
        ),
      ),
    );
  }
}

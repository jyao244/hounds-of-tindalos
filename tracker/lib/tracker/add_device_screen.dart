import 'package:dio/dio.dart';
import 'package:flutter/material.dart';
import 'package:tracker/utils/httpManager.dart';
import 'package:tracker/utils/localDb.dart';
import 'package:tracker/widgets/text_field_input.dart';

import '../utils/colors.dart';
import 'add_related_details_screen.dart';

class AddDeviceScreen extends StatefulWidget {
  @override
  State<AddDeviceScreen> createState() => _AddDeviceScreenState();
}

class _AddDeviceScreenState extends State<AddDeviceScreen> {
  final TextEditingController _imeiController = TextEditingController();
  bool _isLoading = false;

  @override
  void dispose() {
    super.dispose();
    _imeiController.dispose();
  }

  void addDevice() async {
    setState(() {
      _isLoading = true;
    });
    String id = LocalDB.getString("id");
    print(id);
    Response response = await HttpManager.post('/elderly/add', data: {
      "userId": id,
      "imei": _imeiController.text,
    });
    var elderlyId = response.data['_id'];
    Navigator.push(
      context,
      MaterialPageRoute(
          builder: (context) => AddRelatedDetailsScreen(elderlyId: elderlyId)),
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
                  const Text(
                    'Please insert IMEI to continue.',
                    style: TextStyle(fontWeight: FontWeight.w600, fontSize: 24),
                  ),
                  // text field input for IMEI
                  TextFieldInput(
                    textEditingController: _imeiController,
                    hintText: "Tracking device's IMEI",
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
                          : const Text("Next",
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
                      'This is the unique identifier of your device,\nwhich is a unique 15-digit code.',
                    ),
                    padding: const EdgeInsets.symmetric(vertical: 12),
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

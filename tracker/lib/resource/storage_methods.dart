import 'dart:typed_data';

import 'package:firebase_storage/firebase_storage.dart';
import 'package:uuid/uuid.dart';

class StorageMethods {
  final FirebaseStorage _storage = FirebaseStorage.instance;

  // add image to firebase storage
  Future<String> uploadImageToStorage(Uint8List file) async {
    // creating location to our firebase storage
    String id = const Uuid().v1();
    String path = 'images/$id';

    Reference ref = _storage.ref().child(path);

    // putting in uint8list format -> Upload task like a future but not future
    UploadTask uploadTask = ref.putData(
        file,
        SettableMetadata(
          contentType: "image/jpeg",
        ));

    TaskSnapshot snapshot = await uploadTask;
    String downloadUrl = await snapshot.ref.getDownloadURL();
    return downloadUrl;
  }
}

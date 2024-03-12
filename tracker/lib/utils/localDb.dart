import 'package:shared_preferences/shared_preferences.dart';

class LocalDB {
  static late SharedPreferences _preferences;

  LocalDB._internal();
  factory LocalDB() => _instance;
  static late final LocalDB _instance = LocalDB._internal();

  static Future<LocalDB> getInstance() async {
    _preferences = await SharedPreferences.getInstance();
    return _instance;
  }

  static Future<bool> setString(String key, String value) {
    return _preferences.setString(key, value);
  }

  static String getString(String key) {
    return _preferences.getString(key) ?? "";
  }

  static Future<bool> setBool(String key, bool value) {
    return _preferences.setBool(key, value);
  }

  static bool getBool(String key) {
    return _preferences.getBool(key) ?? false;
  }

  static Future<bool> setInt(String key, int value) {
    return _preferences.setInt(key, value);
  }

  static int getInt(
    String key,
  ) {
    return _preferences.getInt(key) ?? 0;
  }

  static Future<bool> setStringList(String key, List<String> value){
    return _preferences.setStringList(key, value);
  }

  static List<String> getStringList(String key) {
    return _preferences.getStringList(key) ?? [];
  }
}

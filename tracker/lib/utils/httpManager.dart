import 'package:dio/dio.dart';
import 'package:tracker/utils/localDb.dart';

import 'global_variables.dart';

class HttpManager {
  static Dio dio = new Dio();

  static Future<Response> get(String url, {prefix = true}) async {
    return await dio.get(prefix ? API_ENDPOINT + url : url);
  }

  static Future<Response> post(String url, {data}) async {
    if (LocalDB.getBool("isLoggedIn")) {
      dio.options.headers["auth-token"] = LocalDB.getString("token");
    }
    return await dio.post(API_ENDPOINT + url, data: data);
  }

  static Future<Response> put(String url, {data}) async {
    if (LocalDB.getBool("isLoggedIn")) {
      dio.options.headers["auth-token"] = LocalDB.getString("token");
    }
    return await dio.put(API_ENDPOINT + url, data: data);
  }

  static Future<Response> delete(String url, {data}) async {
    if (LocalDB.getBool("isLoggedIn")) {
      dio.options.headers["auth-token"] = LocalDB.getString("token");
    }
    return await dio.delete(API_ENDPOINT + url, data: data);
  }
}

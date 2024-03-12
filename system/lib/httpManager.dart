import 'package:dio/dio.dart';

class HttpManager {
  static Dio dio = new Dio();

  // static const API_ENDPOINT = "http://10.0.2.2:3001/api/"; //this one is for emulator
  static const API_ENDPOINT = "http://localhost:3001/api/";

  static Future<Response> get(String url, {prefix = true}) async {
    return await dio.get(prefix ? API_ENDPOINT + url : url);
  }

  static Future<Response> post(String url, {data}) async {
    return await dio.post(API_ENDPOINT + url, data: data);
  }

  static Future<Response> put(String url, {data}) async {
    return await dio.put(API_ENDPOINT + url, data: data);
  }

  static Future<Response> delete(String url, {data}) async {
    return await dio.delete(API_ENDPOINT + url, data: data);
  }
}

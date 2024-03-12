using System.Text;
using Newtonsoft.Json;

namespace tracker;

public class Utils
{
    private static readonly string GOOGLE_GEOLOCATION_API =
        "https://www.googleapis.com/geolocation/v1/geolocate?key=xxx"; // put your own google api key here

    private static readonly HttpClient client = new();

    public static async Task<dynamic> getLocationByWIFI(string WIFIString)
    {
        // fetch geolocation
        var WIFIAccessPoints = new List<object>();
        var WIFIList = WIFIString.Split('&');
        foreach (var t in WIFIList)
            WIFIAccessPoints.Add(new
            {
                macAddress = t.Split(",")[0],
                signalStrength = t.Split(",")[1]
            });
        // use google geolocation endpoint to fetch gps location
        var json = JsonConvert.SerializeObject(new { considerIp = false, wifiAccessPoints = WIFIAccessPoints });
        var responseMessage = await client.PostAsync(GOOGLE_GEOLOCATION_API,
            new StringContent(json, Encoding.UTF8, "application/json"));
        var content = await responseMessage.Content.ReadAsStringAsync();

        // return the location info
        dynamic result = JsonConvert.DeserializeObject(content);
        return result;
    }


    public static async Task<dynamic> getLocationByLBS(string LBSString)
    {
        // fetch geolocation
        var cellTowers = new List<object>();
        var LBSList = LBSString.Split('|');
        var t = LBSList[0];
            cellTowers.Add(new
            {
                cellId = t.Split(",")[0],
                locationAreaCode = t.Split(",")[1],
                mobileCountryCode = t.Split(",")[3],
                mobileNetworkCode = t.Split(",")[2],
                signalStrength = t.Split(",")[4]
            });
        // use google geolocation endpoint to fetch location
        var json = JsonConvert.SerializeObject(new { considerIp = false, cellTowers });
        var responseMessage = await client.PostAsync(GOOGLE_GEOLOCATION_API,
            new StringContent(json, Encoding.UTF8, "application/json"));
        var content = await responseMessage.Content.ReadAsStringAsync();

        // return the location info
        dynamic result = JsonConvert.DeserializeObject(content);
        return result;
    }
}
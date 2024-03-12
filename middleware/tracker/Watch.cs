using System.Text;
using H002.Model;
using Newtonsoft.Json;
using tracker;

namespace H002.SDK;

/// <summary>
///     send data to backend code here
/// </summary>
public class Watch
{
    private readonly string BASE_URL = "http://localhost:3001/api";
    private readonly HttpClient client = new();
    private readonly List<byte[]> CmdCodeArray = new();

    /// <summary>
    ///     handle watch
    /// </summary>
    /// <param name="receivedData">The uploaded string obtained by the TCP server</param>
    public Watch(string receivedData)
    {
        //Determine if an offline command has been issued
        var Ds = DataState.End;
        var Items = receivedData.Split(new[] { "@B#@", "@E#@" }, StringSplitOptions.RemoveEmptyEntries);
        for (var m = 0; m < Items.Length; m++)
        {
            if (Items.Length < 1)
                return;
            var cmdstr = "@B#@" + Items[m] + "@E#@";
            var data = cmdstr.Split('|');
            if (data.Length == 1)
                // this is the empty message, which is caused by the TCP msg window size
                return;
            var protocol = data[2];
            var IMEI = data[3];
            //Get offline commands based on IMEI
            // CmdCodeArray = GetOfflineCommandProcess(IMEI);
            if (CmdCodeArray != null && CmdCodeArray.Count > 0) Ds = DataState.Accept;

            switch (protocol)
            {
                case "001":
                    Console.WriteLine("Received location msg, invoke location handler");
                    GetLocation(Ds, cmdstr);
                    break;
                case "003":
                    var m3 = new C003(cmdstr);
                    break;
                case "005":
                    var m5 = new C005(cmdstr);
                    break;
                case "006":
                    Console.WriteLine("Received emergency msg, invoke notification handler");
                    GetNotification(Ds, cmdstr);
                    break;
                case "010":
                    Console.WriteLine("Received blood pressure info, invoke blood pressure handler");
                    GetBloodPressure(Ds, cmdstr);
                    break;
                case "012":
                    Console.WriteLine("Received heart rate info, invoke heart rate handler");
                    GetHeartRate(Ds, cmdstr);
                    break;
                case "044":
                    break;
                case "031":
                    Console.WriteLine("Received blood oxygen info, invoke blood oxygen handler");
                    GetBloodOxygen(Ds, cmdstr);
                    break;
                case "061":
                    Console.WriteLine("Received body temperature info, invoke body temperature handler");
                    GetTemperature(Ds, cmdstr);
                    break;
            }
        }
    }

    public List<byte[]> getConfirmMessages()
    {
        return CmdCodeArray;
    }


    /// <summary>
    ///     Uplink current location packets Operational processing
    /// </summary>
    /// <param name="cmdStr">Incoming function codes</param>
    /// <param name="rbyte">Returned confirmation command</param>
    /// <returns>Location models</returns>
    public async void GetLocation(DataState Ds,
        string cmdStr = "@B#@|01|001|111112222233333|8888888888888888|1|55|20160715150323|125.48276|37.615124|1|@E#@")

    {
        try
        {
            //Converting command codes to object models
            var m = new C001(cmdStr);
            if (m.Protocol == null)
                //Failed to parse
                return;

            #region Corresponding business process Confirmation code reply

            var rm = new C002(cmdStr);
            rm.Protocol = "01";
            rm.DataState = (int)Ds;
            rm.IMEI = m.IMEI;
            rm.TerminalTime = DateTime.UtcNow.ToString("yyyyMMddhhmmss");
            rm.DataState = 0;
            var rstr = rm.ToString();
            var rbyte = Encoding.UTF8.GetBytes(rstr);
            CmdCodeArray.Add(rbyte);

            // init the variable
            var longitude = "";
            var latitude = "";

            switch (m.LbsType)
            {
                case 1:
                {
                    // send the GPS information to the backend from here
                    var temp = m.GpsString.Split('|');
                    longitude = temp[1];
                    latitude = "-" + temp[0];
                    break;
                }
                case 2:
                {
                    // fetch geolocation by using LBS
                    var raw = await Utils.getLocationByLBS(m.CellString);
                    longitude = raw["location"]["lng"].ToString();
                    latitude = raw["location"]["lat"].ToString();
                    break;
                }
                case 3:
                {
                    // fetch geolocation by using WIFI
                    var jObject = await Utils.getLocationByWIFI(m.WifiCellString.Split("|")[0]);
                    longitude = jObject["location"]["lng"].ToString();
                    latitude = jObject["location"]["lat"].ToString();
                    break;
                }
            }

            Console.WriteLine(latitude + ' ' + longitude);

            // date format
            var date = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss") + "Z";

            var url = BASE_URL + "/data/add/location";
            var json = JsonConvert.SerializeObject(new
                { imei = m.IMEI, longitude, latitude, date, wear = m.WearState, battery = m.Power });
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            await client.PostAsync(url, data);
            Console.WriteLine("Send the GPS info to the backend");

            #endregion
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public async void GetNotification(DataState Ds,
        string cmdStr = "@B#@|01|006|111112222233333|8888888888888888|20160715153805|116.322987|39.983424|1|1|@E#@")

    {
        try
        {
            //Converting command codes to object models
            var m = new C006(cmdStr);
            if (m.Protocol == null)
                //Failed to parse
                return;

            #region Corresponding business process Confirmation code reply

            var rm = new C007(cmdStr);
            rm.Protocol = "01";
            rm.DataState = (int)Ds;
            rm.IMEI = m.IMEI;
            rm.TerminalTime = DateTime.UtcNow.ToString("yyyyMMddhhmmss");
            rm.DataState = 0;
            var rstr = rm.ToString();
            var rbyte = Encoding.UTF8.GetBytes(rstr);
            CmdCodeArray.Add(rbyte);

            var longitude = "";
            var latitude = "";

            switch (m.LbsType)
            {
                case 1:
                {
                    // send the GPS information to the backend from here
                    var temp = m.GpsString.Split('|');
                    longitude = temp[1];
                    latitude = "-" + temp[0];
                    break;
                }
                case 2:
                {
                    // fetch geolocation by using LBS
                    var raw = await Utils.getLocationByLBS(m.CellString);
                    longitude = raw["location"]["lng"].ToString();
                    latitude = raw["location"]["lat"].ToString();
                    break;
                }
                case 3:
                {
                    // fetch geolocation by using WIFI
                    var jObject = await Utils.getLocationByWIFI(m.WifiCellString.Split("|")[0]);
                    longitude = jObject["location"]["lng"].ToString();
                    latitude = jObject["location"]["lat"].ToString();
                    break;
                }
            }

            Console.WriteLine(latitude + ' ' + longitude);

            // date format
            var date = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss") + "Z";

            var url = BASE_URL + "/notification/add";
            var json = JsonConvert.SerializeObject(new
            {
                imei = m.IMEI, date, title = "SOS Message", latitude, longitude,
                subtitle = " has pressed the SOS button"
            });
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            await client.PostAsync(url, data);
            Console.WriteLine("Send the notification info to the backend");

            #endregion
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public async void GetBloodPressure(DataState Ds,
        string cmdStr = "@B#@|01|010|111112222233333|8888888888888888|1|75|86|120|20160715162252|@E#@")

    {
        try
        {
            //Converting command codes to object models
            var m = new C010(cmdStr);
            if (m.Protocol == null)
                //Failed to parse
                return;

            #region Corresponding business process Confirmation code reply

            var rm = new C011(cmdStr);
            rm.Protocol = "01";
            rm.DataState = (int)Ds;
            rm.IMEI = m.IMEI;
            rm.TerminalTime = DateTime.UtcNow.ToString("yyyyMMddhhmmss");
            rm.DataState = 0;
            var rstr = rm.ToString();
            var rbyte = Encoding.UTF8.GetBytes(rstr);
            CmdCodeArray.Add(rbyte);

            //IMEI
            Console.WriteLine("IMEI：" + m.IMEI);

            // date format
            var date = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss") + "Z";

            var url = BASE_URL + "/data/add/bloodpressure";
            var json = JsonConvert.SerializeObject(new
                { imei = m.IMEI, date, highPressure = m.SBP, lowPressure = m.DBP });
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            await client.PostAsync(url, data);
            Console.WriteLine("Send the blood pressure info to the backend");

            #endregion
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public async void GetHeartRate(DataState Ds,
        string cmdStr =
            "@B#@|01|012|111112222233333|8888888888888888|1|32|89|980|201612162330&201612170730&0210&0550|20160715162252|@E#@")

    {
        try
        {
            //Converting command codes to object models
            var m = new C012(cmdStr);
            if (m.Protocol == null)
                //Failed to parse
                return;

            #region Corresponding business process Confirmation code reply

            var rm = new C013(cmdStr);
            rm.Protocol = "01";
            rm.DataState = (int)Ds;
            rm.IMEI = m.IMEI;
            rm.TerminalTime = DateTime.UtcNow.ToString("yyyyMMddhhmmss");
            rm.DataState = 0;
            var rstr = rm.ToString();
            var rbyte = Encoding.UTF8.GetBytes(rstr);
            CmdCodeArray.Add(rbyte);

            //IMEI
            Console.WriteLine("IMEI：" + m.IMEI);

            var temp = m.Sleep.Split('&');

            if (temp[0] == "0")
            {
                temp[0] = "202201010000";
            }
            if (temp[1] == "0")
            {
                temp[1] = "202201010000";
            }
            
            // sleep start & end time
            var ts = DateTime.Now - DateTime.ParseExact(m.TerminalTime, "yyyyMMddHHmmss", null);
            var startTime = DateTime.ParseExact(temp[0], "yyyyMMddHHmm", null).Add(ts);
            var endTime = DateTime.ParseExact(temp[1], "yyyyMMddHHmm", null).Add(ts);
            var startSleep = startTime.ToString("yyyy-MM-ddTHH:mm") + "Z";
            var endSleep = endTime.ToString("yyyy-MM-ddTHH:mm") + "Z";

            // time parser (current 0210 -> 2 hours and 10 mins)
            var hours = int.Parse(temp[2]) / 100;
            var mins = int.Parse(temp[2]) - hours * 100;
            var deepSleep = hours * 60 + mins;

            hours = int.Parse(temp[3]) / 100;
            mins = int.Parse(temp[3]) - hours * 100;
            var lightSleep = hours * 60 + mins;

            // date format
            var date = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss") + "Z";

            var url = BASE_URL + "/data/add/heartrate";
            var json = JsonConvert.SerializeObject(new
            {
                imei = m.IMEI, date, heartRate = m.Bat, steps = m.BuShu, deepSleep, lightSleep, startSleep, endSleep
            });
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            await client.PostAsync(url, data);
            Console.WriteLine("Send the heart rate & steps & sleep info to the backend");

            #endregion
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public async void GetBloodOxygen(DataState Ds,
        string cmdStr =
            "@B#@|01|012|111112222233333|8888888888888888|1|32|89|980|201612162330&201612170730&0210&0550|20160715162252|@E#@")

    {
        try
        {
            //Converting command codes to object models
            var m = new C031(cmdStr);
            if (m.Protocol == null)
            {
                Console.WriteLine("031 error");
                //Failed to parse
                return;
            }

            #region Corresponding business process Confirmation code reply

            var rm = new C032(cmdStr);
            rm.Protocol = "01";
            rm.WearState = (int)Ds;
            rm.IMEI = m.IMEI;
            rm.TerminalTime = DateTime.UtcNow.ToString("yyyyMMddhhmmss");
            var rstr = rm.ToString();
            var rbyte = Encoding.UTF8.GetBytes(rstr);
            CmdCodeArray.Add(rbyte);

            //IMEI
            Console.WriteLine("IMEI：" + m.IMEI);

            // date format
            var date = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss") + "Z";

            var url = BASE_URL + "/data/add/bloodoxygen";
            var json = JsonConvert.SerializeObject(new
                { imei = m.IMEI, date, bloodOxygen = m.BloodOxygen });
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            await client.PostAsync(url, data);
            Console.WriteLine("Send the blood oxygen info to the backend");

            #endregion
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public async void GetTemperature(DataState Ds,
        string cmdStr =
            "@B#@|01|012|111112222233333|8888888888888888|1|32|89|980|201612162330&201612170730&0210&0550|20160715162252|@E#@")

    {
        try
        {
            //Converting command codes to object models
            var m = new C061(cmdStr);
            if (m.Protocol == null)
                //Failed to parse
                return;

            #region Corresponding business process Confirmation code reply

            var rm = new C062(cmdStr);
            rm.Protocol = "01";
            rm.IMEI = m.IMEI;
            rm.Time = DateTime.UtcNow.ToString("yyyyMMddhhmmss");
            rm.DataStatus = "0";
            var rstr = rm.ToString();
            var rbyte = Encoding.UTF8.GetBytes(rstr);
            CmdCodeArray.Add(rbyte);

            //IMEI
            Console.WriteLine("IMEI：" + m.IMEI);

            // date format
            var date = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss") + "Z";

            var url = BASE_URL + "/data/add/temperature";
            var json = JsonConvert.SerializeObject(new
                { imei = m.IMEI, date, temp = m.Temperature });
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            await client.PostAsync(url, data);
            Console.WriteLine("Send the body temperature info to the backend");

            #endregion
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
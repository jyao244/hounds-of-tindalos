namespace H002.Model;

/// <summary>
///     Upload Warning packet
///     Association confirmation command FunCode: 007
/// </summary>
public class C006
{
    /// <summary>
    ///     Function code
    /// </summary>
    public const string FunCode = "006";

    /// <summary>
    ///     End packet @E#@
    /// </summary>
    public string FootCode = "@E#@";

    /// <summary>
    ///     Start Fixed value :@B#@
    /// </summary>
    public string HeadCode = "@B#@";


    public C006()
    {
    }

    /// <summary>
    ///     @B#@|01|006|111112222233333|8888888888888888|20160715153805|116.322987|39.983424|1|1|@E#@
    /// </summary>
    /// <param name="cmdStr">command code</param>
    public C006(string cmdStr)
    {
        var data = cmdStr.Split('|');
        var dataLength = data.Length;
        if (dataLength > 9 && data[0] == "@B#@" && FunCode == data[2])
        {
            HeadCode = data[0];
            Protocol = data[1];
            IMEI = data[3];
            IMSI = data[4];
            TerminalTime = data[5];

            LbsType = int.Parse(data[dataLength - 3]);
            AlarmType = int.Parse(data[dataLength - 2]);
            FootCode = data[dataLength - 1];
            //GPS data
            if (LbsType == 1)
            {
                var lat = Convert.ToDecimal(data[6]);
                var lon = Convert.ToDecimal(data[7]);
                GpsString = lat + "|" + lon;
            }

            //lbs data
            if (LbsType == 2)
            {
                #region LBS analysis

                CellString = "";
                for (var i = 6; i < dataLength - 3; i++)
                    if (i >= dataLength - 3)
                        CellString += data[i];
                    else
                        CellString += data[i] + "|";

                #endregion
            }

            //wifi data+lbs data
            if (LbsType == 3) WifiCellString = data[6] + "|" + data[7];
        }
    }

    /// <summary>
    ///     Protocol version number
    /// </summary>
    public string Protocol { get; set; }

    /// <summary>
    ///     International Mobile Equipment Identity
    /// </summary>
    public string IMEI { get; set; }

    /// <summary>
    ///     International Mobile Subscriber Identification Number
    /// </summary>
    public string IMSI { get; set; }

    /// <summary>
    ///     The terminal current time ( format:yyyyMMddHHmmss)
    /// </summary>
    public string TerminalTime { get; set; }

    /// <summary>
    ///     When LbsType (geographical location type) was 1 (GPS) format:   latitude|longitude
    /// </summary>
    public string GpsString { get; set; }

    /// <summary>
    ///     When LbsType (geographic location type) was 2 (cell),  upload format:
    ///     cell_id,lac,mnc,mcc,rssi|cell_id,lac,mnc,mcc,rssi|...
    /// </summary>
    public string CellString { get; set; }

    /// <summary>
    ///     When LbsType (geographic location type) was 2 (wifi+cell),  upload format:  1c:fa:56:b5,-61&
    ///     1c:fa:56:b5,-61|cell_id,lac,mnc,mcc,rss&cell_id,lac,mnc,mcc,rss
    /// </summary>
    public string WifiCellString { get; set; }

    /// <summary>
    ///     Location type 1:GPS location position 2: base station location location 3:WIFI+ base station location
    /// </summary>
    public int LbsType { get; set; }

    /// <summary>
    ///     Warning type 1: red alarm 2: yellow alarm 3: Green alarm
    /// </summary>
    public int AlarmType { get; set; }

    public override string ToString()
    {
        var rString = HeadCode + "|";
        rString += Protocol + "|";
        rString += FunCode + "|";
        rString += IMEI + "|";
        rString += IMSI + "|";
        rString += TerminalTime + "|";
        if (LbsType == 1) rString += GpsString + "|";
        if (LbsType == 2) rString += CellString + "|";
        if (LbsType == 3) rString += WifiCellString + "|";
        rString += LbsType + "|";
        rString += AlarmType + "|";
        rString += FootCode;
        return rString;
    }
}
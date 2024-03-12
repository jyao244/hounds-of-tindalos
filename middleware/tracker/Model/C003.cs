namespace H002.Model;

/// <summary>
///     Uplink boot data package
///     Association confirmation command FunCode: 004
/// </summary>
public class C003
{
    /// <summary>
    ///     Function code
    /// </summary>
    public const string FunCode = "003";

    /// <summary>
    ///     End packet @E#@
    /// </summary>
    public string FootCode = "@E#@";

    /// <summary>
    ///     Start Fixed value :@B#@
    /// </summary>
    public string HeadCode = "@B#@";

    public C003()
    {
    }

    /// <summary>
    /// </summary>
    /// <param name="cmdStr">command code</param>
    public C003(string cmdStr)
    {
        //@B#@|01|003|111112222233333|8888888888888888|1.0.1|1|55|20160715150323 | 125.48276 | 37.615124 | 1 | @E#@
        var data = cmdStr.Split('|');
        var dataLength = data.Length;
        //min length is 13
        if (dataLength >= 12 && data[0] == "@B#@" && FunCode == data[2])
        {
            HeadCode = data[0];
            Protocol = data[1];
            IMEI = data[3];
            IMSI = data[4];
            WearVersion = data[5];
            WearState = int.Parse(data[6]);
            Power = int.Parse(data[7]);
            TerminalTime = data[8];
            LbsType = int.Parse(data[dataLength - 2]);
            FootCode = data[dataLength - 1];
            //GPS data
            if (LbsType == 1)
            {
                var lat = Convert.ToDecimal(data[9]);
                var lon = Convert.ToDecimal(data[10]);
                GpsString = lat + "|" + lon;
            }

            //lbs data
            if (LbsType == 2)
            {
                #region LBS analysis

                CellString = "";
                var lbsCount = data.Length - 11;
                for (var i = 0; i < lbsCount; i++)
                    if (i >= lbsCount - 1)
                        CellString += data[9 + i];
                    else
                        CellString += data[9 + i] + "|";

                #endregion
            }

            //wifi data+lbs data
            if (LbsType == 3) WifiCellString = data[9] + "|" + data[10];
        }
        else
        {
            Protocol = null;
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
    ///     Terminal version
    /// </summary>
    public string WearVersion { get; set; } //terminal version 1.0.1

    /// <summary>
    ///     Terminal wear state  (0: is not worn; 1: is worn)
    /// </summary>
    public int WearState { get; set; }

    /// <summary>
    ///     Percentage of residual electricity (range:1-100)
    /// </summary>
    public int Power { get; set; } //power 1-100

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

    public override string ToString()
    {
        var rString = HeadCode + "|";
        rString += Protocol + "|";
        rString += FunCode + "|";
        rString += IMEI + "|";
        rString += IMSI + "|";
        rString += WearVersion + "|";
        rString += WearState + "|";
        rString += Power + "|";
        rString += TerminalTime + "|";
        if (LbsType == 1) rString += GpsString + "|";
        if (LbsType == 2) rString += CellString + "|";
        if (LbsType == 3) rString += WifiCellString + "|";
        rString += LbsType + "|";
        rString += FootCode;
        return rString;
    }
}
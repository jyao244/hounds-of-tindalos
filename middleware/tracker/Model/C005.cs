namespace H002.Model;

/// <summary>
///     Upside off data packet
///     Unrelated command
/// </summary>
public class C005
{
    /// <summary>
    ///     Function code
    /// </summary>
    public const string FunCode = "005";

    /// <summary>
    ///     End packet @E#@
    /// </summary>
    public string FootCode = "@E#@";

    /// <summary>
    ///     Start Fixed value :@B#@
    /// </summary>
    public string HeadCode = "@B#@";


    public C005()
    {
    }

    /// <summary>
    ///     @B#@|01|005|111112222233333|8888888888888888|55|20160715150323|125.48276|37.615124|1|@E#@
    /// </summary>
    /// <param name="cmdStr">command code</param>
    public C005(string cmdStr)
    {
        var data = cmdStr.Split('|');
        var dataLength = data.Length;
        if (dataLength > 10 && data[0] == "@B#@" && FunCode == data[2])
        {
            HeadCode = data[0];
            Protocol = data[1];
            IMEI = data[3];
            IMSI = data[4];
            Power = int.Parse(data[5]);
            TerminalTime = data[6];

            LbsType = int.Parse(data[dataLength - 2]);
            FootCode = data[dataLength - 1];
            //GPS data
            if (LbsType == 1)
            {
                var lat = Convert.ToDecimal(data[7]);
                var lon = Convert.ToDecimal(data[8]);
                GpsString = lat + "|" + lon;
            }

            //lbs data
            if (LbsType == 2)
            {
                #region LBS analysis

                CellString = "";
                for (var i = 7; i < dataLength - 2; i++)
                    if (i == dataLength - 3)
                        CellString += data[i];
                    else
                        CellString += data[i] + "|";

                #endregion
            }

            //wifi data+lbs data
            if (LbsType == 3) WifiCellString = data[7] + "|" + data[8];
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
        rString += Power + "|";
        rString += TerminalTime + "|";
        rString += GpsString + "|";
        rString += CellString + "|";
        rString += WifiCellString + "|";
        rString += LbsType + "|";
        rString += FootCode;
        return rString;
    }
}
namespace H002.Model;

/// <summary>
///     Uplink heart rate abnormal alarm data packet
///     Association confirmation command FunCode: 030
/// </summary>
public class C029
{
    /// <summary>
    ///     Function code
    /// </summary>
    public const string FunCode = "029";

    /// <summary>
    ///     End packet @E#@
    /// </summary>
    public string FootCode = "@E#@";

    /// <summary>
    ///     Start Fixed value :@B#@
    /// </summary>
    public string HeadCode = "@B#@";


    public C029()
    {
    }

    /// <summary>
    ///     @B#@|01|029|111112222233333|8888888888888888|1|32|1|103|113.960415|22.574777|1|20160716103002|@E#@
    /// </summary>
    /// <param name="cmdStr">command code</param>
    public C029(string cmdStr)
    {
        var data = cmdStr.Split('|');
        var dataLength = data.Length;
        if (dataLength > 11 && data[0] == "@B#@" && FunCode == data[2])
        {
            HeadCode = data[0];
            Protocol = data[1];
            IMEI = data[3];
            IMSI = data[4];
            WearState = int.Parse(data[5]);
            Power = int.Parse(data[6]);
            SosType = int.Parse(data[7]);
            Bat = int.Parse(data[8]);

            LbsType = int.Parse(data[dataLength - 3]);
            TerminalTime = data[dataLength - 2];
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
                var lbsCount = data.Length - 3;
                for (var i = 9; i < lbsCount; i++)
                    if (i >= lbsCount)
                        CellString += data[i];
                    else
                        CellString += data[i] + "|";

                #endregion
            }

            //wifi data+lbs data
            if (LbsType == 3) WifiCellString = data[9] + "|" + data[10];
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
    ///     Terminal wear state  (0: is not worn; 1: is worn)
    /// </summary>
    public int WearState { get; set; }

    /// <summary>
    ///     Percentage of residual electricity (range:1-100)
    /// </summary>
    public int Power { get; set; } //power 1-100

    /// <summary>
    ///     1 heart rate alarm 2 no action alarm
    /// </summary>
    public int SosType { get; set; }

    /// <summary>
    ///     The average value of all the heart rate measured in the heart rate 60s
    /// </summary>
    public int Bat { get; set; }

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
    ///     The terminal current time ( format:yyyyMMddHHmmss)
    /// </summary>
    public string TerminalTime { get; set; }


    public override string ToString()
    {
        var rString = HeadCode + "|";
        rString += Protocol + "|";
        rString += FunCode + "|";
        rString += IMEI + "|";
        rString += IMSI + "|";
        rString += Power + "|";
        rString += SosType + "|";
        rString += Bat + "|";
        if (LbsType == 1) rString += GpsString + "|";
        if (LbsType == 2) rString += CellString + "|";
        if (LbsType == 3) rString += WifiCellString + "|";
        rString += LbsType + "|";
        rString += TerminalTime + "|";
        rString += FootCode;
        return rString;
    }
}
namespace H002.Model;

/// <summary>
///     Uplink terminal number information packet
///     Associated downlink confirmation 034
/// </summary>
public class C033
{
    /// <summary>
    ///     Function code
    /// </summary>
    public const string FunCode = "033";

    /// <summary>
    ///     End packet @E#@
    /// </summary>
    public string FootCode = "@E#@";

    /// <summary>
    ///     Start Fixed value :@B#@
    /// </summary>
    public string HeadCode = "@B#@";


    public C033()
    {
    }

    /// <summary>
    ///     @B#@|01|033|359872071150311|9460028880160509|1|39|20180622104446|@E#@
    /// </summary>
    /// <param name="cmdStr">command code</param>
    public C033(string cmdStr)
    {
        var data = cmdStr.Split('|');
        var dataLength = data.Length;
        if (dataLength > 8 && data[0] == "@B#@" && FunCode == data[2])
        {
            HeadCode = data[0];
            Protocol = data[1];
            IMEI = data[3];
            IMSI = data[4];
            WearState = int.Parse(data[5]);
            Power = int.Parse(data[6]);
            TerminalTime = data[7];
            FootCode = data[8];
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
        rString += WearState + "|";
        rString += Power + "|";
        rString += TerminalTime + "|";
        rString += FootCode;
        return rString;
    }
}
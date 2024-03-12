namespace H002.Model;

public class C032
{
    /// <summary>
    ///     Function code
    /// </summary>
    public const string FunCode = "032";

    /// <summary>
    ///     End packet @E#@
    /// </summary>
    public string FootCode = "@E#@";

    /// <summary>
    ///     Start Fixed value :@B#@
    /// </summary>
    public string HeadCode = "@B#@";

    public C032()
    {
    }

    /// <summary>
    ///     @B#@|01|001|359872071150311|9460028880160509|1|55|20180622040727|12835,22476,0,460,9|32835,22476,0,460,8|2|@E#@
    /// </summary>
    /// <param name="cmdStr">command code</param>
    public C032(string cmdStr)
    {
        var data = cmdStr.Split('|');
        var dataLength = data.Length;
        HeadCode = data[0];
        Protocol = data[1];
        IMEI = data[3];
        IMSI = data[4];
        WearState = int.Parse(data[5]);
        Power = int.Parse(data[6]);
        BloodOxygen = data[7];
        TerminalTime = data[8];
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
    ///     International Mobile Subscriber Identification Number
    /// </summary>
    public string BloodOxygen { get; set; }

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
        rString += WearState + "|";
        rString += TerminalTime + "|";
        rString += FootCode;
        return rString;
    }
}
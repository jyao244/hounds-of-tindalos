namespace H002.Model;

/// <summary>
///     Uplink low electricity alarm data packet
///     Unassociated confirmation package
/// </summary>
public class C028
{
    /// <summary>
    ///     Function code
    /// </summary>
    public const string FunCode = "028";

    /// <summary>
    ///     End packet @E#@
    /// </summary>
    public string FootCode = "@E#@";

    /// <summary>
    ///     Start Fixed value :@B#@
    /// </summary>
    public string HeadCode = "@B#@";


    public C028()
    {
    }

    /// <summary>
    ///     @B#@|01|028|111112222233333|8888888888888888|15|1|20160715162720|@E#@
    /// </summary>
    /// <param name="cmdStr">command code</param>
    public C028(string cmdStr)
    {
        var data = cmdStr.Split('|');
        var dataLength = data.Length;
        if (dataLength > 8 && data[0] == "@B#@" && FunCode == data[2])
        {
            HeadCode = data[0];
            Protocol = data[1];
            IMEI = data[3];
            IMSI = data[4];
            Power = int.Parse(data[5]);
            WearState = int.Parse(data[6]);
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
    ///     Percentage of residual electricity (range:1-100)
    /// </summary>
    public int Power { get; set; } //power 1-100

    /// <summary>
    ///     Terminal wear state  (0: is not worn; 1: is worn)
    /// </summary>
    public int WearState { get; set; }

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
        rString += WearState + "|";
        rString += TerminalTime + "|";
        rString += FootCode;
        return rString;
    }
}
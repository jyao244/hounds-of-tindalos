namespace H002.Model;

/// <summary>
///     Upstream SMS trigger request packet No downlink acknowledgement
///     a.This request should keep the 60S network link waiting for the server to send response data.
///     b.The server's downstream response data interface is any one of the downlink interfaces in this document. The
///     terminal shall handle the interface according to the corresponding interface requirements.
///     c.Since short message operators send SMS messages that are not in the form of a pure key (messages will be prefixed
///     with a short message mark), after receiving a short message, it is necessary to search the full text of the message
///     to see if it contains both agreed-upon key fields and follow-up.deal with.
/// </summary>
public class C039
{
    /// <summary>
    ///     Function code
    /// </summary>
    public const string FunCode = "039";

    /// <summary>
    ///     End packet @E#@
    /// </summary>
    public string FootCode = "@E#@";

    /// <summary>
    ///     Start Fixed value :@B#@
    /// </summary>
    public string HeadCode = "@B#@";


    public C039()
    {
    }

    /// <summary>
    ///     @B#@|01|039|111112222233333|8888888888888888|20160715153805|1|54|@E#@
    /// </summary>
    /// <param name="cmdStr">command code</param>
    public C039(string cmdStr)
    {
        var data = cmdStr.Split('|');
        var dataLength = data.Length;
        if (dataLength > 8 && data[0] == "@B#@" && FunCode == data[2])
        {
            HeadCode = data[0];
            Protocol = data[1];
            IMEI = data[3];
            IMSI = data[4];
            TerminalTime = data[5];
            WearState = int.Parse(data[6]);
            Power = int.Parse(data[7]);
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
    ///     The terminal current time ( format:yyyyMMddHHmmss)
    /// </summary>
    public string TerminalTime { get; set; }

    /// <summary>
    ///     Terminal wear state  (0: is not worn; 1: is worn)
    /// </summary>
    public int WearState { get; set; }

    /// <summary>
    ///     Percentage of residual electricity (range:1-100)
    /// </summary>
    public int Power { get; set; } //power 1-100

    public override string ToString()
    {
        var rString = HeadCode + "|";
        rString += Protocol + "|";
        rString += FunCode + "|";
        rString += IMEI + "|";
        rString += IMSI + "|";
        rString += TerminalTime + "|";
        rString += WearState + "|";
        rString += Power + "|";
        rString += FootCode;
        return rString;
    }
}
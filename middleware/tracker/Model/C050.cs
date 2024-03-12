namespace H002.Model;

/// <summary>
///     Uplink time zone setting confirmation pack
///     Associative downstream 049
/// </summary>
public class C050
{
    /// <summary>
    ///     Function code
    /// </summary>
    public const string FunCode = "050";

    /// <summary>
    ///     End packet @E#@
    /// </summary>
    public string FootCode = "@E#@";

    /// <summary>
    ///     Start Fixed value :@B#@
    /// </summary>
    public string HeadCode = "@B#@";


    public C050()
    {
    }

    /// <summary>
    ///     @B#@|01|047|111112222233333|8888888888888888|20160715153805|0255BB90395C475E9D6155AF98B383875|@E#@
    ///     Upload log:@B#@|01|050|359872071150311|9460028880160509|20180706133250|cfa75460ebe b0e4e||@E#@
    /// </summary>
    /// <param name="cmdStr">command code</param>
    public C050(string cmdStr)
    {
        var data = cmdStr.Split('|');
        var dataLength = data.Length;
        if (dataLength > 7 && data[0] == "@B#@" && FunCode == data[2])
        {
            HeadCode = data[0];
            Protocol = data[1];
            IMEI = data[3];
            IMSI = data[4];
            TerminalTime = data[5];
            TaskId = data[6];
            FootCode = data[7];
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
    ///     Task identity, user custom, convenient return identification
    /// </summary>
    public string TaskId { get; set; }


    public override string ToString()
    {
        var rString = HeadCode + "|";
        rString += Protocol + "|";
        rString += FunCode + "|";
        rString += IMEI + "|";
        rString += IMSI + "|";
        rString += TerminalTime + "|";
        rString += TaskId + "|";
        rString += FootCode;
        return rString;
    }
}
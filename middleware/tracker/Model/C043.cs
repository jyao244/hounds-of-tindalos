namespace H002.Model;

/// <summary>
///     Upstream Service Center Configuration Data Confirmation Package
///     Associated downlink  042
/// </summary>
public class C043
{
    /// <summary>
    ///     Function code
    /// </summary>
    public const string FunCode = "043";

    /// <summary>
    ///     End packet @E#@
    /// </summary>
    public string FootCode = "@E#@";

    /// <summary>
    ///     Start Fixed value :@B#@
    /// </summary>
    public string HeadCode = "@B#@";

    public C043()
    {
    }

    /// <summary>
    ///     @B#@|01|043|111112222233333|20160715153805|@E#@
    /// </summary>
    /// <param name="cmdStr">command code</param>
    public C043(string cmdStr)
    {
        var data = cmdStr.Split('|');
        var dataLength = data.Length;
        if (dataLength > 5 && data[0] == "@B#@" && FunCode == data[2])
        {
            HeadCode = data[0];
            Protocol = data[1];
            IMEI = data[3];
            TerminalTime = data[4];
            FootCode = data[5];
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
    ///     The terminal current time ( format:yyyyMMddHHmmss)
    /// </summary>
    public string TerminalTime { get; set; }

    public override string ToString()
    {
        var rString = HeadCode + "|";
        rString += Protocol + "|";
        rString += FunCode + "|";
        rString += IMEI + "|";
        rString += TerminalTime + "|";
        rString += FootCode;
        return rString;
    }
}
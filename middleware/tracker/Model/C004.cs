namespace H002.Model;

/// <summary>
///     Downlink boot confirmation data package
///     Association  command  FunCode: 003
/// </summary>
public class C004
{
    /// <summary>
    ///     Function code
    /// </summary>
    public const string FunCode = "004";

    /// <summary>
    ///     End packet @E#@
    /// </summary>
    public string FootCode = "@E#@";

    /// <summary>
    ///     Start Fixed value :@B#@
    /// </summary>
    public string HeadCode = "@B#@";


    public C004()
    {
    }

    /// <summary>
    ///     @B#@|01|004|111112222233333|0|20160729174051|@E#@
    /// </summary>
    /// <param name="cmdStr">command code</param>
    public C004(string cmdStr = "@B#@|01|004|111112222233333|0|20160729174051|@E#@")
    {
        var data = cmdStr.Split('|');
        var dataLength = data.Length;
        if (dataLength > 6 && data[0] == "@B#@" && FunCode == data[2])
        {
            HeadCode = data[0];
            Protocol = data[1];
            IMEI = data[3];
            DataState = int.Parse(data[4]);
            TerminalTime = data[5];
            FootCode = data[6];
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
    ///     Terminal wear state  (0: is not worn; 1: is worn)
    /// </summary>
    public int DataState { get; set; }

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
        rString += DataState + "|";
        rString += TerminalTime + "|";
        rString += FootCode;
        return rString;
    }
}
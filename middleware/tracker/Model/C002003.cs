namespace H002.Model;

/// <summary>
///     Downlink confirmation location data package
///     Association  command  FunCode: 001
/// </summary>
public class C002003
{
    /// <summary>
    ///     Function code
    /// </summary>
    public const string FunCode = "002";

    /// <summary>
    ///     End packet @E#@
    /// </summary>
    public string FootCode = "@E#@";

    /// <summary>
    ///     Start Fixed value :@B#@
    /// </summary>
    public string HeadCode = "@B#@";

    public C002003()
    {
    }

    /// <summary>
    ///     @B#@|01|002|111112222233333|0|\u5317\u4eac\u5e02\u6d77\u6dc0\u533a\u4e2d\u5173\u6751\u5927\u8857\u0032\u0037\u53f7\u0031\u0031\u0030\u0031\u002d\u0030\u0038\u5ba4|20160729173850|@E#@
    /// </summary>
    /// <param name="cmdStr">command code</param>
    public C002003(
        string cmdStr =
            "@B#@|01|002|111112222233333|0|\u5317\u4eac\u5e02\u6d77\u6dc0\u533a\u4e2d\u5173\u6751\u5927\u8857\u0032\u0037\u53f7\u0031\u0031\u0030\u0031\u002d\u0030\u0038\u5ba4|20160729173850|@E#@")
    {
        var data = cmdStr.Split('|');
        var dataLength = data.Length;
        if (dataLength > 7 && data[0] == "@B#@" && FunCode == data[2])
        {
            HeadCode = data[0];
            Protocol = data[1];
            IMEI = data[3];
            DataState = int.Parse(data[4]);
            Address = data[5];
            TerminalTime = data[6];
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
    ///     location Unicode code(Unicode-UTF8)
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    ///     Data state 0: end; 1: continue to accept
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
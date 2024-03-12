namespace H002.Model;

/// <summary>
///     Downlink confirmation location data package
///     Association  command  FunCode: 001
/// </summary>
public class C002
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

    public C002()
    {
    }

    /// <summary>
    ///     @B#@|01|002|111112222233333|0|20160729173850|@E#@
    /// </summary>
    /// <param name="cmdStr">command code</param>
    public C002(string cmdStr = "@B#@|01|002|111112222233333|0|20160729173850|@E#@")
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
        // location info, not important but we require this one
        rString +=
            "6D596C5F770100205B816CE25E020020911E5DDE533A0020949F516C5E9953578DEF002097608FD14E5D66F25C0F533A4E8C671F|";
        rString += TerminalTime + "|";
        rString += FootCode;
        return rString;
    }
}
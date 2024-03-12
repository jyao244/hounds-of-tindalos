namespace H002.Model;

/// <summary>
///     Association  command  FunCode: 010
/// </summary>
public class C011
{
    /// <summary>
    ///     Function code
    /// </summary>
    public const string FunCode = "011";

    /// <summary>
    ///     End packet @E#@
    /// </summary>
    public string FootCode = "@E#@";

    /// <summary>
    ///     Start Fixed value :@B#@
    /// </summary>
    public string HeadCode = "@B#@";


    public C011()
    {
    }

    /// <summary>
    ///     @B#@|01|011|111112222233333|0|20160729190919|@E#@
    /// </summary>
    /// <param name="cmdStr">command code</param>
    public C011(string cmdStr)
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
        rString += TerminalTime + "|";
        rString += FootCode;
        return rString;
    }
}
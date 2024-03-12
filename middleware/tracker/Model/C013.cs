namespace H002.Model;

/// <summary>
///     Downlink heart rate / step / sleep data validation packet
///     Association  command  FunCode: 012
/// </summary>
public class C013
{
    /// <summary>
    ///     Function code
    /// </summary>
    public const string FunCode = "013";

    /// <summary>
    ///     End packet @E#@
    /// </summary>
    public string FootCode = "@E#@";

    /// <summary>
    ///     Start Fixed value :@B#@
    /// </summary>
    public string HeadCode = "@B#@";


    public C013()
    {
    }

    /// <summary>
    ///     @B#@|01|002|111112222233333|0|20160729173850|@E#@
    /// </summary>
    /// <param name="cmdStr">command code</param>
    public C013(string cmdStr)
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
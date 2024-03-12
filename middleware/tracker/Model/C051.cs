namespace H002.Model;

/// <summary>
/// Downlink remote shutdown package
/// Associated uplink confirmation 051
/// </summary>
public class C051
{
    /// <summary>
    ///     Function code
    /// </summary>
    public const string FunCode = "051";

    /// <summary>
    ///     End packet @E#@
    /// </summary>
    public string FootCode = "@E#@";

    /// <summary>
    ///     Start Fixed value :@B#@
    /// </summary>
    public string HeadCode = "@B#@";


    public C051()
    {
    }

    /// <summary>
    ///     @B#@|01|051|111112222233333|0|20160729192431|0255BB90395C475E9D6155AF98B38387|@E#@
    /// </summary>
    /// <param name="cmdStr">command code</param>
    public C051(string cmdStr)
    {
        var data = cmdStr.Split('|');
        var dataLength = data.Length;
        if (dataLength > 7 && data[0] == "@B#@" && FunCode == data[2])
        {
            HeadCode = data[0];
            Protocol = data[1];
            IMEI = data[3];
            DataState = int.Parse(data[4]);
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
    ///     Data state 0: end; 1: continue to accept
    /// </summary>
    public int DataState { get; set; }

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
        rString += DataState + "|";
        rString += TerminalTime + "|";
        rString += TaskId + "|";
        rString += FootCode;
        return rString;
    }
}
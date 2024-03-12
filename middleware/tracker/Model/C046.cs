namespace H002.Model;

/// <summary>
///     Downside alarm clock configuration package
///     Associative Upstream Confirmation 047
/// </summary>
public class C046
{
    /// <summary>
    ///     Function code
    /// </summary>
    public const string FunCode = "046";

    /// <summary>
    ///     End packet @E#@
    /// </summary>
    public string FootCode = "@E#@";

    /// <summary>
    ///     Start Fixed value :@B#@
    /// </summary>
    public string HeadCode = "@B#@";


    public C046()
    {
    }

    /// <summary>
    ///     @B#@|01|046|111112222233333|0&1&0830&1111111%1&1&1630&11 11100|
    ///     20160715153805|0255BB90395C475E9D6155AF98B383875|@E#@
    /// </summary>
    /// <param name="cmdStr">command code</param>
    public C046(string cmdStr)
    {
        var data = cmdStr.Split('|');
        var dataLength = data.Length;
        if (dataLength > 7 && data[0] == "@B#@" && FunCode == data[2])
        {
            HeadCode = data[0];
            Protocol = data[1];
            IMEI = data[3];
            Ring = data[4];
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
    ///     Example: 0&1&0830&1111111%1&1&1630&1111100
    ///     Alarm type [0: medication reminder 1: water reminder 2: exercise reminder 3: custom 1] & alarm switch [0 for off, 1 for on] & alarm time & repeat pattern [11111100 for working Monday to Friday, not working Saturday / Sunday]
    ///     Multiple sets of alarms separated by %
    /// </summary>
    public string Ring { get; set; }

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
        rString += Ring + "|";
        rString += TerminalTime + "|";
        rString += TaskId + "|";
        rString += FootCode;
        return rString;
    }
}
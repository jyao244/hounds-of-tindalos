namespace H002.Model;

/// <summary>
///     Downlink heart rate |GPS data cycle setting data package
///     Association  command  FunCode: 015
/// </summary>
public class C014
{
    /// <summary>
    ///     Function code
    /// </summary>
    public const string FunCode = "014";

    /// <summary>
    ///     End packet @E#@
    /// </summary>
    public string FootCode = "@E#@";

    /// <summary>
    ///     Start Fixed value :@B#@
    /// </summary>
    public string HeadCode = "@B#@";


    public C014()
    {
    }

    /// <summary>
    ///     @B#@|01|014|111112222233333|1|10|20160715162720|0255BB90395C475E9D6155AF98B38387|@E#@
    /// </summary>
    /// <param name="cmdStr">command code</param>
    public C014(string cmdStr)
    {
        var data = cmdStr.Split('|');
        var dataLength = data.Length;
        if (dataLength > 8 && data[0] == "@B#@" && FunCode == data[2])
        {
            HeadCode = data[0];
            Protocol = data[1];
            IMEI = data[3];
            CycleType = int.Parse(data[4]);
            CycleTime = int.Parse(data[5]);
            TerminalTime = data[6];
            TaskId = data[7];
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
    ///     Cycle type 1: cycle 2:GPS cycle of heart rate
    /// </summary>
    public int CycleType { get; set; }

    /// <summary>
    /// </summary>
    public int CycleTime { get; set; } //minutes 0-300

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
        rString += CycleType + "|";
        rString += CycleTime + "|";
        rString += TerminalTime + "|";
        rString += TaskId + "|";
        rString += FootCode;
        return rString;
    }
}
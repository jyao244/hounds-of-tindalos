namespace H002.Model;

/// <summary>
///     The balance of the | downlink GPS silent voice broadcast | low battery alert time set packet
///     Association confirmation command FunCode: 017
/// </summary>
public class C016
{
    /// <summary>
    ///     Function code
    /// </summary>
    public const string FunCode = "016";

    /// <summary>
    ///     End packet @E#@
    /// </summary>
    public string FootCode = "@E#@";

    /// <summary>
    ///     Start Fixed value :@B#@
    /// </summary>
    public string HeadCode = "@B#@";

    public C016()
    {
    }

    /// <summary>
    ///     @B#@|01|016|111112222233333|2|2100|0600|20160715162720|0255BB90395C475E9D6155AF98B38387|@E#@
    /// </summary>
    /// <param name="cmdStr">command code</param>
    public C016(string cmdStr)
    {
        var data = cmdStr.Split('|');
        var dataLength = data.Length;
        //min length is 12
        if (dataLength > 9 && data[0] == "@B#@" && FunCode == data[2])
        {
            HeadCode = data[0];
            Protocol = data[1];
            IMEI = data[3];
            SetType = int.Parse(data[4]);
            BeginTime = data[5];
            OverTime = data[6];
            TerminalTime = data[7];
            TaskId = data[8];
            FootCode = data[9];
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
    ///     1: GPS silent 2: voice broadcast 3: low power reminder settings type
    /// </summary>
    public int SetType { get; set; }

    /// <summary>
    ///     At the start time, the insufficient bit is filled with 0 (range:0000-2359)
    /// </summary>
    public string BeginTime { get; set; }

    /// <summary>
    ///     At the end of the time, the insufficient bit is filled with 0 (range:0000-2359)
    /// </summary>
    public string OverTime { get; set; }

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
        rString += SetType + "|";
        rString += BeginTime + "|";
        rString += OverTime + "|";
        rString += TerminalTime + "|";
        rString += TaskId + "|";
        rString += FootCode;
        return rString;
    }
}
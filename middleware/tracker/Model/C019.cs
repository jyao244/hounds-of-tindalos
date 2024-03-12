namespace H002.Model;

/// <summary>
///     Uplink logical switch configuration confirmation data package
///     Association confirmation command FunCode: 018
/// </summary>
public class C019
{
    /// <summary>
    ///     Function code
    /// </summary>
    public const string FunCode = "019";

    /// <summary>
    ///     End packet @E#@
    /// </summary>
    public string FootCode = "@E#@";

    /// <summary>
    ///     Start Fixed value :@B#@
    /// </summary>
    public string HeadCode = "@B#@";


    public C019()
    {
    }

    /// <summary>
    ///     @B#@|01|019|111112222233333|8888888888888888|1|20160715153805|0255BB90395C475E9D6155AF98B383875|@E#@
    /// </summary>
    /// <param name="cmdStr">command code</param>
    public C019(string cmdStr)
    {
        var data = cmdStr.Split('|');
        var dataLength = data.Length;
        if (dataLength > 8 && data[0] == "@B#@" && FunCode == data[2])
        {
            HeadCode = data[0];
            Protocol = data[1];
            IMEI = data[3];
            IMSI = data[4];
            SwitchType = int.Parse(data[5]);
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
    ///     International Mobile Subscriber Identification Number
    /// </summary>
    public string IMSI { get; set; }

    /// <summary>
    ///     1:GPS switch
    ///     2: white list switch
    ///     3: Huang Jian short message switch
    ///     4: red key short message switch
    /// </summary>
    public int SwitchType { get; set; }

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
        rString += IMSI + "|";
        rString += SwitchType + "|";
        rString += TerminalTime + "|";
        rString += TaskId + "|";
        rString += FootCode;
        return rString;
    }
}
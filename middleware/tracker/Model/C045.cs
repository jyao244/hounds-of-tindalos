namespace H002.Model;

/// <summary>
///     Uplink voice file configuration confirmation packet
///     Link Down 044
/// </summary>
public class C045
{
    /// <summary>
    ///     Function code
    /// </summary>
    public const string FunCode = "045";

    /// <summary>
    ///     End packet @E#@
    /// </summary>
    public string FootCode = "@E#@";

    /// <summary>
    ///     Start Fixed value :@B#@
    /// </summary>
    public string HeadCode = "@B#@";


    public C045()
    {
    }

    /// <summary>
    ///     @B#@|01|045|359872071150311|9460028880160509|3|1|1|20180706142651|767ce40339ba7f63||@E#@
    ///     @B#@|01|045|111112222233333|8888888888888888|1|20160715153805|0255BB90395C475E9D6155AF98B383875|@E#@
    /// </summary>
    /// <param name="cmdStr">command code</param>
    public C045(string cmdStr)
    {
        var data = cmdStr.Split('|');
        var dataLength = data.Length;
        if (dataLength > 10 && data[0] == "@B#@" && FunCode == data[2])
        {
            HeadCode = data[0];
            Protocol = data[1];
            IMEI = data[3];
            IMSI = data[4];
            PackageCount = int.Parse(data[5]);
            PackageIndex = int.Parse(data[6]);
            packOver = int.Parse(data[7]);
            TerminalTime = data[8];
            TaskId = data[9];
            FootCode = data[10];
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
    ///     If the voice file is delivered in 2 packages, it is filled here. 02
    /// </summary>
    public int PackageCount { get; set; }

    /// <summary>
    ///     Packet Index
    /// </summary>
    public int PackageIndex { get; set; }

    /// <summary>
    ///     Completed 0: Not completed 1: Completed
    /// </summary>
    public int packOver { get; set; }

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
        rString += PackageCount + "|";
        rString += PackageIndex + "|";
        rString += packOver + "|";
        rString += TerminalTime + "|";
        rString += TaskId + "|";
        rString += FootCode;
        return rString;
    }
}
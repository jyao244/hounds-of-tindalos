namespace H002.Model;

/// <summary>
///     Downstream positioning request packet
///     Associated upstream confirmation 001
/// </summary>
public class C048
{
    /// <summary>
    ///     Function code
    /// </summary>
    public const string FunCode = "048";

    /// <summary>
    ///     End packet @E#@
    /// </summary>
    public string FootCode = "@E#@";

    /// <summary>
    ///     Start Fixed value :@B#@
    /// </summary>
    public string HeadCode = "@B#@";


    public C048()
    {
    }

    /// <summary>
    ///     @B#@|01|048|111112222233333|0255BB90395C475E9D6155AF98B38387|@E#@
    /// </summary>
    /// <param name="cmdStr">command code</param>
    public C048(string cmdStr)
    {
        var data = cmdStr.Split('|');
        var dataLength = data.Length;
        if (dataLength > 5 && data[0] == "@B#@" && FunCode == data[2])
        {
            HeadCode = data[0];
            Protocol = data[1];
            IMEI = data[3];
            TaskId = data[4];
            FootCode = data[5];
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
    ///     Task identity, user custom, convenient return identification
    /// </summary>
    public string TaskId { get; set; }


    public override string ToString()
    {
        var rString = HeadCode + "|";
        rString += Protocol + "|";
        rString += FunCode + "|";
        rString += IMEI + "|";
        rString += TaskId + "|";
        rString += FootCode;
        return rString;
    }
}
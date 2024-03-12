namespace H002.Model;

/// <summary>
///     Heart rate alarm upper and lower limit settings
/// </summary>
public class C053
{
    /// <summary>
    ///     Function code
    /// </summary>
    public const string FunCode = "053";

    /// <summary>
    ///     End packet @E#@
    /// </summary>
    public string FootCode = "@E#@";

    /// <summary>
    ///     Start Fixed value :@B#@
    /// </summary>
    public string HeadCode = "@B#@";


    public C053()
    {
    }

    /// <summary>
    ///     @B#@|01|053|111112222233333|45-150|0255BB90395C475E9D6155AF98B383875|@E#@
    /// </summary>
    /// <param name="cmdStr">command code</param>
    public C053(string cmdStr)
    {
        var data = cmdStr.Split('|');
        var dataLength = data.Length;
        if (dataLength > 6 && data[0] == "@B#@" && FunCode == data[2])
        {
            HeadCode = data[0];
            Protocol = data[1];
            IMEI = data[3];
            HeartScope = data[4];
            TaskId = data[5];
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
    ///     报警上下限
    ///     下限-上限，两个值之间用-分隔，如45-150
    /// </summary>
    public string HeartScope { get; set; }

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
        rString += HeartScope + "|";
        rString += TaskId + "|";
        rString += FootCode;
        return rString;
    }
}
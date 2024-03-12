namespace H002.Model;

/// <summary>
///     Downside time zone setting package
///     Associated upstream confirmation 050
/// </summary>
public class C049
{
    /// <summary>
    ///     Function code
    /// </summary>
    public const string FunCode = "049";

    /// <summary>
    ///     End packet @E#@
    /// </summary>
    public string FootCode = "@E#@";

    /// <summary>
    ///     Start Fixed value :@B#@
    /// </summary>
    public string HeadCode = "@B#@";


    public C049()
    {
    }

    /// <summary>
    ///     @B#@|01|049|111112222233333|+08|0255BB90395C475E9D6155AF98B38387|@E#@
    /// </summary>
    /// <param name="cmdStr">command code</param>
    public C049(string cmdStr)
    {
        var data = cmdStr.Split('|');
        var dataLength = data.Length;
        if (dataLength > 6 && data[0] == "@B#@" && FunCode == data[2])
        {
            HeadCode = data[0];
            Protocol = data[1];
            IMEI = data[3];
            TimeZone = int.Parse(data[4]);
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
    ///     +08, Beijing time
    /// </summary>
    public int TimeZone { get; set; }

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
        rString += TimeZone + "|";
        rString += TaskId + "|";
        rString += FootCode;
        return rString;
    }
}
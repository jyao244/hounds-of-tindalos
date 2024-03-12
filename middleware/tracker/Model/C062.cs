namespace H002.Model;

/// <summary>
///     Uplink body temperature data package
/// </summary>
public class C062
{
    /// <summary>
    ///     Function code
    /// </summary>
    public const string FunCode = "062";

    /// <summary>
    ///     End packet @E#@
    /// </summary>
    public string FootCode = "@E#@";

    /// <summary>
    ///     Start Fixed value :@B#@
    /// </summary>
    public string HeadCode = "@B#@";


    public C062()
    {
    }

    /// <summary>
    ///     @B#@|01|062|111112222233333|0|20160729190919|@E#@
    /// </summary>
    /// <param name="cmdStr">command code</param>
    public C062(string cmdStr)
    {
        var data = cmdStr.Split('|');
        var dataLength = data.Length;
        if (dataLength > 6 && data[0] == "@B#@" && FunCode == data[2])
        {
            HeadCode = data[0];
            Protocol = data[1];
            IMEI = data[3];
            DataStatus = data[4];
            Time = data[5];
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
    ///     data status
    ///     International Mobile Subscriber Identification Number
    /// </summary>
    public string DataStatus { get; set; }

    /// <summary>
    ///     time
    /// </summary>
    public string Time { get; set; }


    public override string ToString()
    {
        var rString = HeadCode + "|";
        rString += Protocol + "|";
        rString += FunCode + "|";
        rString += IMEI + "|";
        rString += DataStatus + "|";
        rString += Time + "|";
        rString += FootCode;
        return rString;
    }
}
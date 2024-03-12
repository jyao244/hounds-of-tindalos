namespace H002.Model;

/// <summary>
///     Downstream instant temperature detection packets
/// </summary>
public class C064
{
    /// <summary>
    ///     Function code
    /// </summary>
    public const string FunCode = "064";

    /// <summary>
    ///     End packet @E#@
    /// </summary>
    public string FootCode = "@E#@";

    /// <summary>
    ///     Start Fixed value :@B#@
    /// </summary>
    public string HeadCode = "@B#@";


    public C064()
    {
    }

    /// <summary>
    ///     @B#@|01|064|111112222233333|8888888888888888|20160715153805|0255BB90395C475E9D6155AF98B383875|@E#@
    /// </summary>
    /// <param name="cmdStr">command code</param>
    public C064(string cmdStr)
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
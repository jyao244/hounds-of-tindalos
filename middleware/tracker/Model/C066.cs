namespace H002.Model;

/// <summary>
///     Downstream instant temperature detection packets
/// </summary>
public class C066
{
    /// <summary>
    ///     Function code
    /// </summary>
    public const string FunCode = "066";

    /// <summary>
    ///     End packet @E#@
    /// </summary>
    public string FootCode = "@E#@";

    /// <summary>
    ///     Start Fixed value :@B#@
    /// </summary>
    public string HeadCode = "@B#@";


    public C066()
    {
    }

    /// <summary>
    ///     @B#@|01|066|111112222233333|8888888888888888|1|20160715153805|0255BB90395C475E9D6155AF98B383875|@E#@
    /// </summary>
    /// <param name="cmdStr">command code</param>
    public C066(string cmdStr)
    {
        var data = cmdStr.Split('|');
        var dataLength = data.Length;
        if (dataLength > 9 && data[0] == "@B#@" && FunCode == data[2])
        {
            HeadCode = data[0];
            Protocol = data[1];
            IMEI = data[3];
            IMSI = data[4];
            AddressUpdateResults = data[5];
            time = data[7];
            Taskid = data[8];
            FootCode = data[9];
        }
    }

    /// <summary>
    ///     Protocol version number
    /// </summary>
    public string Protocol { get; set; }

    /// <summary>
    /// </summary>
    public string IMEI { get; set; }

    /// <summary>
    ///     IMSI
    /// </summary>
    public string IMSI { get; set; }

    /// <summary>
    ///     address update results
    /// </summary>
    public string AddressUpdateResults { get; set; }

    /// <summary>
    ///     time
    /// </summary>
    public string time { get; set; }

    /// <summary>
    ///     task id
    /// </summary>
    public string Taskid { get; set; }


    public override string ToString()
    {
        var rString = HeadCode + "|";
        rString += Protocol + "|";
        rString += FunCode + "|";
        rString += IMEI + "|";

        rString += FootCode;
        return rString;
    }
}
namespace H002.Model;

/// <summary>
///     Downlink heart rate / behavior abnormal alarm confirmation data package
///     Association  command FunCode: 029
/// </summary>
public class C030
{
    /// <summary>
    ///     Function code
    /// </summary>
    public const string FunCode = "030";

    /// <summary>
    ///     End packet @E#@
    /// </summary>
    public string FootCode = "@E#@";

    /// <summary>
    ///     Start Fixed value :@B#@
    /// </summary>
    public string HeadCode = "@B#@";


    public C030()
    {
    }

    /// <summary>
    ///     @B#@|01|030|111112222233333|0|\u6df1\u5733\u5e02\u9f99\u5c97\u533a\u534e\u4e3a\u767e\u8349\u56ed|Tom|20160729192103|@E#@
    /// </summary>
    /// <param name="cmdStr">command code</param>
    public C030(string cmdStr)
    {
        var data = cmdStr.Split('|');
        var dataLength = data.Length;
        if (dataLength > 8 && data[0] == "@B#@" && FunCode == data[2])
        {
            HeadCode = data[0];
            Protocol = data[1];
            IMEI = data[3];
            DataState = int.Parse(data[4]);
            AddressUnicode = data[5];
            NameUnicode = data[6];
            TerminalTime = data[7];
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
    ///     Terminal wear state  (0: is not worn; 1: is worn)
    /// </summary>
    public int DataState { get; set; }

    /// <summary>
    ///     Position Unicode coding
    /// </summary>
    public string AddressUnicode { get; set; }

    /// <summary>
    ///     Name Unicode code
    /// </summary>
    public string NameUnicode { get; set; }

    /// <summary>
    ///     The terminal current time ( format:yyyyMMddHHmmss)
    /// </summary>
    public string TerminalTime { get; set; }


    public override string ToString()
    {
        var rString = HeadCode + "|";
        rString += Protocol + "|";
        rString += FunCode + "|";
        rString += IMEI + "|";
        rString += DataState + "|";
        rString += AddressUnicode + "|";
        rString += NameUnicode + "|";
        rString += TerminalTime + "|";
        rString += FootCode;
        return rString;
    }
}
namespace H002.Model;

/// <summary>
///     Downlink button alarm confirmation packet
///     Association  command  FunCode: 006
/// </summary>
public class C007
{
    /// <summary>
    ///     Function code
    /// </summary>
    public const string FunCode = "007";

    /// <summary>
    ///     End packet @E#@
    /// </summary>
    public string FootCode = "@E#@";

    /// <summary>
    ///     Start Fixed value :@B#@
    /// </summary>
    public string HeadCode = "@B#@";


    public C007()
    {
    }

    /// <summary>
    ///     @B#@|01|007|111112222233333|0
    ///     |\u5317\u4eac\u5e02\u6d77\u6dc0\u533a\u4e2d\u5173\u6751\u5927\u8857\u0032\u0037\u53f7\u0031\u0031\u0030\u0031\u002d\u0030\u0038\u5ba4
    ///     |Tom|20160805172500|@E#@
    /// </summary>
    /// <param name="cmdStr">command code</param>
    public C007(
        string cmdStr =
            "@B#@|01|007|111112222233333|0|\u5317\u4eac\u5e02\u6d77\u6dc0\u533a\u4e2d\u5173\u6751\u5927\u8857\u0032\u0037\u53f7\u0031\u0031\u0030\u0031\u002d\u0030\u0038\u5ba4|Tom|20160805172500|@E#@")
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
            Name = data[6];
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
    ///     Terminal address Unicode-UTF8)
    /// </summary>
    public string AddressUnicode { get; set; }

    /// <summary>
    ///     Terminal wearer's name (Unicode-UTF8)
    /// </summary>
    public string Name { get; set; }

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
        rString += Name + "|";
        rString += TerminalTime + "|";
        rString += FootCode;
        return rString;
    }
}
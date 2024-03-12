namespace H002.Model;

/// <summary>
///     Uplink body temperature data package
/// </summary>
public class C061
{
    /// <summary>
    ///     Function code
    /// </summary>
    public const string FunCode = "061";

    /// <summary>
    ///     End packet @E#@
    /// </summary>
    public string FootCode = "@E#@";

    /// <summary>
    ///     Start Fixed value :@B#@
    /// </summary>
    public string HeadCode = "@B#@";


    public C061()
    {
    }

    /// <summary>
    ///     @B#@|01|061|111112222233333|8888888888888888|1|75|36.5|20160715162252|@E#@
    /// </summary>
    /// <param name="cmdStr">command code</param>
    public C061(string cmdStr)
    {
        var data = cmdStr.Split('|');
        var dataLength = data.Length;
        if (dataLength > 9 && data[0] == "@B#@" && FunCode == data[2])
        {
            HeadCode = data[0];
            Protocol = data[1];
            IMEI = data[3];
            IMSI = data[4];
            State = data[5];
            ElectricQuantity = data[6];
            Temperature = data[7];
            Time = data[8];
            FootCode = data[9];
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
    ///     state
    /// </summary>
    public string State { get; set; }

    /// <summary>
    ///     power
    /// </summary>
    public string ElectricQuantity { get; set; }

    /// <summary>
    ///     temperature
    /// </summary>
    public string Temperature { get; set; }

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
        rString += IMSI + "|";
        rString += State + "|";
        rString += ElectricQuantity + "|";
        rString += Temperature + "|";
        rString += Time + "|";
        rString += FootCode;
        return rString;
    }
}
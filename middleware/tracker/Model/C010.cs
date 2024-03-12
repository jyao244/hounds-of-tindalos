namespace H002.Model;

/// <summary>
///     010
///     Upside family number setting confirmation package
///     Association  command  FunCode: 008
/// </summary>
public class C010
{
    /// <summary>
    ///     Function code
    /// </summary>
    public const string FunCode = "010";

    /// <summary>
    ///     End packet @E#@
    /// </summary>
    public string FootCode = "@E#@";

    /// <summary>
    ///     Start Fixed value :@B#@
    /// </summary>
    public string HeadCode = "@B#@";

    public C010()
    {
    }

    /// <summary>
    ///     @B#@|01|010|111112222233333|8888888888888888|1|75|86|120|20160715162252|@E#@
    /// </summary>
    /// <param name="cmdStr">command code</param>
    public C010(string cmdStr)
    {
        var data = cmdStr.Split('|');
        var dataLength = data.Length;
        if (dataLength > 10 && data[0] == "@B#@" && FunCode == data[2])
        {
            HeadCode = data[0];
            Protocol = data[1];
            IMEI = data[3];
            IMSI = data[4];
            WearState = int.Parse(data[5]);
            Power = int.Parse(data[6]);
            DBP = float.Parse(data[7]);
            SBP = float.Parse(data[8]);
            TerminalTime = data[9];
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
    ///     Terminal wear state  (0: is not worn; 1: is worn)
    /// </summary>
    public int WearState { get; set; }

    /// <summary>
    ///     Percentage of residual electricity (range:1-100)
    /// </summary>
    public int Power { get; set; }

    /// <summary>
    ///     Diastolic pressure
    /// </summary>
    public float DBP { get; set; }

    /// <summary>
    ///     Systolic pressure
    /// </summary>
    public float SBP { get; set; }

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
        rString += IMSI + "|";
        rString += WearState + "|";
        rString += Power + "|";
        rString += DBP + "|";
        rString += SBP + "|";
        rString += TerminalTime + "|";
        rString += FootCode;
        return rString;
    }
}
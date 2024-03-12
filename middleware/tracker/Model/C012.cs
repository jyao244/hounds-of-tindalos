namespace H002.Model;

/// <summary>
///     Up heart rate / step / sleep packet
///     Association confirmation command FunCode: 013
/// </summary>
public class C012
{
    /// <summary>
    ///     Function code
    /// </summary>
    public const string FunCode = "012";

    /// <summary>
    ///     End packet @E#@
    /// </summary>
    public string FootCode = "@E#@";

    /// <summary>
    ///     Start Fixed value :@B#@
    /// </summary>
    public string HeadCode = "@B#@";


    public C012()
    {
    }

    /// <summary>
    ///     @B#@|01|012|111112222233333|8888888888888888|1|32|89|980|201612162330&201612170730&0210&0550|20160715162252|@E#@
    /// </summary>
    /// <param name="cmdStr">command code</param>
    public C012(string cmdStr)
    {
        var data = cmdStr.Split('|');
        var dataLength = data.Length;
        //min length is 12
        if (dataLength > 11 && data[0] == "@B#@" && FunCode == data[2])
        {
            HeadCode = data[0];
            Protocol = data[1];
            IMEI = data[3];
            IMSI = data[4];
            WearState = int.Parse(data[5]);
            Power = int.Parse(data[6]);
            Bat = int.Parse(data[7]);
            BuShu = int.Parse(data[8]);
            Sleep = data[9];
            TerminalTime = data[10];
            FootCode = data[11];
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
    public int Power { get; set; } //power 1-100

    /// <summary>
    ///     Heart rate value
    /// </summary>
    public int Bat { get; set; } //心率值

    /// <summary>
    ///     The number of walking
    /// </summary>
    public int BuShu { get; set; } //步数

    /// <summary>
    ///     The start time of sleep & the end of sleep & deep sleep time & shallow sleep time.
    ///     This data is sent once a day, sent after getting up, and no data is empty.
    /// </summary>
    public string Sleep { get; set; }

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
        rString += Bat + "|";
        rString += BuShu + "|";
        rString += Sleep + "|";
        rString += TerminalTime + "|";
        rString += FootCode;
        return rString;
    }
}
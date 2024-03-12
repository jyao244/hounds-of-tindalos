namespace H002.Model;

/// <summary>
///     Down service center configuration data package
///     Associated upstream confirmation 043
/// </summary>
public class C042
{
    /// <summary>
    ///     Function code
    /// </summary>
    public const string FunCode = "042";

    /// <summary>
    ///     End packet @E#@
    /// </summary>
    public string FootCode = "@E#@";

    /// <summary>
    ///     Start Fixed value :@B#@
    /// </summary>
    public string HeadCode = "@B#@";


    public C042()
    {
    }

    /// <summary>
    ///     @B#@|01|042|111112222233333|5|10|00002359|00002300|01001800|1|1|132 45225552&15223636632|10086&\u0031\u0030\u0032
    ///     |7|45&150|1362215222
    ///     2|15962225212|17052225452|0|20160729174051|@E#@
    /// </summary>
    /// <param name="cmdStr">command code</param>
    public C042(string cmdStr)
    {
        var data = cmdStr.Split('|');
        var dataLength = data.Length;
        if (dataLength > 20 && data[0] == "@B#@" && FunCode == data[2])
        {
            HeadCode = data[0];
            Protocol = data[1];
            IMEI = data[3];
            BatCycle = int.Parse(data[4]);
            GPSCycle = int.Parse(data[5]);
            GPSOffTime = data[6];
            YeTime = data[7];
            LowpTime = data[8];
            IsGPS = int.Parse(data[9]);
            IsBmd = int.Parse(data[10]);
            Bmd = data[11];
            YeSms = data[12];
            Volume = int.Parse(data[13]);
            BatFanWei = data[14];
            RMobile = data[15];
            YMobile = data[16];
            GMobile = data[17];
            UpdateV = int.Parse(data[18]);
            TerminalTime = data[19];
            FootCode = data[20];
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
    ///     Heart rate upload period 0-300 points
    /// </summary>
    public int BatCycle { get; set; }

    /// <summary>
    ///     GPS upload period 0-300 points
    /// </summary>
    public int GPSCycle { get; set; }

    /// <summary>
    ///     GPS silent time setting 07002200
    /// </summary>
    public string GPSOffTime { get; set; }

    /// <summary>
    ///     Balance reminder period setting 07002200
    /// </summary>
    public string YeTime { get; set; }

    /// <summary>
    ///     Low reminder period setting 07002200
    /// </summary>
    public string LowpTime { get; set; }

    /// <summary>
    ///     GPS switch settings 0 off 1 on
    /// </summary>
    public int IsGPS { get; set; }

    /// <summary>
    ///     White list switch setting 0 off 1 on
    /// </summary>
    public int IsBmd { get; set; }

    /// <summary>
    ///     White list number & number
    /// </summary>
    public string Bmd { get; set; }

    /// <summary>
    ///     Balance SMS inquiry     Receive number & SMS
    /// </summary>
    public string YeSms { get; set; }

    /// <summary>
    ///     Volume Level 0-11
    /// </summary>
    public int Volume { get; set; }

    /// <summary>
    ///     Heart rate range 50&150
    /// </summary>
    public string BatFanWei { get; set; }

    /// <summary>
    ///     Red number
    /// </summary>
    public string RMobile { get; set; }

    /// <summary>
    ///     Yellow number
    /// </summary>
    public string YMobile { get; set; }

    /// <summary>
    ///     Green number
    /// </summary>
    public string GMobile { get; set; }

    /// <summary>
    ///     0: No upgrade, 1 upgrade
    /// </summary>
    public int UpdateV { get; set; }

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
        rString += BatCycle + "|" + GPSCycle + "|" + GPSOffTime + "|" + YeTime + "|" + LowpTime + "|" + IsGPS + "|" +
                   IsBmd + "|" + Bmd + "|" + YeSms + "|" + Volume + "|" +
                   BatFanWei + "|" + RMobile + "|" + YMobile + "|" + GMobile + "|" + UpdateV + "|";
        rString += TerminalTime + "|";
        rString += FootCode;
        return rString;
    }
}
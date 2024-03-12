namespace H002.Model;

/// <summary>
///     Downstream weather pack
/// </summary>
public class C060
{
    /// <summary>
    ///     Function code
    /// </summary>
    public const string FunCode = "060";

    /// <summary>
    ///     End packet @E#@
    /// </summary>
    public string FootCode = "@E#@";

    /// <summary>
    ///     Start Fixed value :@B#@
    /// </summary>
    public string HeadCode = "@B#@";


    public C060()
    {
    }

    /// <summary>
    ///     @B#@|01|060|111112222233333|8888888888888888|20160715153805|15|10|19|1|b6e0d4c6|316df15733|@E#@
    /// </summary>
    /// <param name="cmdStr">command code</param>
    public C060(string cmdStr)
    {
        var data = cmdStr.Split('|');
        var dataLength = data.Length;
        if (dataLength > 12 && data[0] == "@B#@" && FunCode == data[2])
        {
            HeadCode = data[0];
            Protocol = data[1];
            IMEI = data[3];
            IMSI = data[4];
            TerminalTime = data[5];
            CurrentTemperature = data[6];
            MinTemperature = data[7];
            MaxTemperature = data[8];
            WeatherType = data[9];
            WeatherDesc = data[10];
            City = data[11];
            FootCode = data[12];
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
    ///     The terminal current time ( format:yyyyMMddHHmmss)
    /// </summary>
    public string TerminalTime { get; set; }

    /// <summary>
    ///     current temp
    /// </summary>
    public string CurrentTemperature { get; set; }

    /// <summary>
    ///     lowest temp
    /// </summary>
    public string MinTemperature { get; set; }

    /// <summary>
    ///     highest temp
    /// </summary>
    public string MaxTemperature { get; set; }

    /// <summary>
    ///     weather type
    /// </summary>
    public string WeatherType { get; set; }

    /// <summary>
    ///     weather desc
    /// </summary>
    public string WeatherDesc { get; set; }

    /// <summary>
    ///     city
    /// </summary>
    public string City { get; set; }


    public override string ToString()
    {
        var rString = HeadCode + "|";
        rString += Protocol + "|";
        rString += FunCode + "|";
        rString += IMEI + "|";
        rString += IMSI + "|";
        rString += TerminalTime + "|";
        rString += CurrentTemperature + "|";
        rString += MinTemperature + "|";
        rString += MaxTemperature + "|";
        rString += WeatherType + "|";
        rString += WeatherDesc + "|";
        rString += City + "|";
        rString += FootCode;
        return rString;
    }
}
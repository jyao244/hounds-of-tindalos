using System.Text;

namespace H002.Model;

/// <summary>
///     Downlink voice file configuration package
///     Associated upstream confirmation 045
/// </summary>
public class C044
{
    /// <summary>
    ///     Function code
    /// </summary>
    public const string FunCode = "044";

    /// <summary>
    ///     Voice format    0: amr
    /// </summary>
    public const int VoiceFormat = 0;

    /// <summary>
    ///     End packet @E#@
    /// </summary>
    public string FootCode = "@E#@";

    /// <summary>
    ///     Start Fixed value :@B#@
    /// </summary>
    public string HeadCode = "@B#@";

    public C044()
    {
    }


    /// <summary>
    ///     package 1：@B#@|01|044|111112222233333|0|0|2040|2|1|1024|[	data	]| 20160715153805|0255BB90395C475E9D6155AF98B38387|@E#@
    /// </summary>
    /// <param name="cmdStr">command code</param>
    public C044(string cmdStr)
    {
        var data = cmdStr.Split('|');
        var dataLength = data.Length;
        if (dataLength > 13 && data[0] == "@B#@" && FunCode == data[2])
        {
            HeadCode = data[0];
            Protocol = data[1];
            IMEI = data[3];
            VoiceType = int.Parse(data[4]);
            VoiceSize = int.Parse(data[6]);
            PackageCount = int.Parse(data[7]);
            PackageIndex = int.Parse(data[8]);
            PackageSize = int.Parse(data[9]);
            VoiceByte = data[10];
            TerminalTime = data[11];
            TaskId = data[12];
            FootCode = data[13];
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
    ///     Voice type 0 medication 1 drink water 2 exercise 3 custom
    /// </summary>
    public int VoiceType { get; set; }

    /// <summary>
    ///     Decimal indicates that the file size is 2040 bytes. Then fill in "2040" here.
    /// </summary>
    public long VoiceSize { get; set; }

    /// <summary>
    ///     If the voice file is delivered in 2 packages, it is filled here. 02
    /// </summary>
    public int PackageCount { get; set; }

    /// <summary>
    ///     Packet Index
    /// </summary>
    public int PackageIndex { get; set; }

    /// <summary>
    ///     Packet size Decimal bytes
    /// </summary>
    public long PackageSize { get; set; }

    /// <summary>
    ///     Voice data [binary stream]
    /// </summary>
    public string VoiceByte { get; set; }

    /// <summary>
    ///     The terminal current time ( format:yyyyMMddHHmmss)
    /// </summary>
    public string TerminalTime { get; set; }

    /// <summary>
    ///     Task identity, user custom, convenient return identification
    /// </summary>
    public string TaskId { get; set; }

    public override string ToString()
    {
        var rString = HeadCode + "|";
        rString += Protocol + "|";
        rString += FunCode + "|";
        rString += IMEI + "|";
        rString += VoiceType + "|" + VoiceFormat + "|" + VoiceSize + "|" + PackageCount +
                   "|" + PackageIndex + "|" + PackageSize + "|" +
                   VoiceByte + "|";
        rString += TerminalTime + "|";
        rString += TaskId + "|";
        rString += FootCode;
        return rString;
    }

    public byte[] ToByte()
    {
        var replyData = new List<byte>();
        replyData.AddRange(
            Encoding.ASCII.GetBytes(
                HeadCode +
                "|" + Protocol +
                "|" + FunCode +
                "|" + IMEI +
                "|" + VoiceType + "|" + VoiceFormat +
                "|" + VoiceSize +
                "|" + PackageCount +
                "|" + PackageIndex +
                "|" + PackageSize + "|"));
        replyData.AddRange(Convert.FromBase64String(VoiceByte));
        replyData.AddRange(
            Encoding.ASCII.GetBytes(
                "|" + DateTime.Now.ToString("yyyyMMddhhmmss") +
                "|" + TaskId +
                "|" + FootCode));
        return replyData.ToArray();
    }

    /// <summary>
    ///     Processing of voice files in default 1024 size sub-packs
    /// </summary>
    /// <param name="VType">voice type</param>
    /// <param name="IMEI">device IMEI</param>
    /// <param name="voiceData">Voice binary streaming</param>
    /// <param name="voicePackLength">Sub-package size Default 1024</param>
    /// <returns>Return to list of all commands after packetization </returns>
    public List<byte[]> ToListByte(int VType, string IMEI, byte[] voiceData, string TaskId, int voicePackLength = 2040)
    {
        var List = new List<byte[]>();
        //Number of dispatches
        var voiceCount = voiceData.Length % voicePackLength == 0
            ? voiceData.Length / voicePackLength
            : voiceData.Length / voicePackLength + 1;
        var voiceNowPageLength = voicePackLength;

        for (var voiceNowIndex = 1; voiceNowIndex <= voiceCount; voiceNowIndex++)
        {
            var voiceIndexData =
                voiceData.ToList()
                    .Skip((voiceNowIndex - 1) * voicePackLength)
                    .Take(voiceNowPageLength)
                    .ToArray();
            if (voiceCount == voiceNowIndex)
                voiceNowPageLength = voiceData.Length % voicePackLength == 0
                    ? voicePackLength
                    : voiceData.Length % voicePackLength;
            var replyData = new List<byte>();
            replyData.AddRange(
                Encoding.ASCII.GetBytes(
                    HeadCode +
                    "|" + Protocol +
                    "|" + FunCode +
                    "|" + IMEI +
                    "|" + VType + "|" + VoiceFormat +
                    "|" + voiceData.Length +
                    "|" + voiceCount +
                    "|" + voiceNowIndex +
                    "|" + voiceNowPageLength + "|"));
            replyData.AddRange(voiceIndexData);
            replyData.AddRange(
                Encoding.ASCII.GetBytes(
                    "|" + DateTime.Now.ToString("yyyyMMddhhmmss") +
                    "|" + TaskId +
                    "|" + FootCode));
            List.Add(replyData.ToArray());
        }

        return List;
    }

    /// <summary>
    ///     Processing of voice files in default 1024 size sub-packs
    /// </summary>
    /// <param name="VType">voice type</param>
    /// <param name="IMEI">device IMEI</param>
    /// <param name="voiceData">Voice binary streaming</param>
    /// <param name="voicePackLength">Sub-package size Default 1024</param>
    /// <returns>Return to list of all commands after packetization </returns>
    public List<string> ToListCmd(int VType, string IMEI, byte[] voiceData, string TaskId, int voicePackLength = 2040)
    {
        var List = new List<string>();
        //Number of dispatches
        var voiceCount = voiceData.Length % voicePackLength == 0
            ? voiceData.Length / voicePackLength
            : voiceData.Length / voicePackLength + 1;
        var voiceNowPageLength = voicePackLength;

        for (var voiceNowIndex = 1; voiceNowIndex <= voiceCount; voiceNowIndex++)
        {
            var voiceIndexData =
                voiceData.ToList()
                    .Skip((voiceNowIndex - 1) * voicePackLength)
                    .Take(voiceNowPageLength)
                    .ToArray();
            if (voiceCount == voiceNowIndex)
                voiceNowPageLength = voiceData.Length % voicePackLength == 0
                    ? voicePackLength
                    : voiceData.Length % voicePackLength;
            var rString = new StringBuilder();

            rString.Append(
                HeadCode +
                "|" + Protocol +
                "|" + FunCode +
                "|" + IMEI +
                "|" + VType +
                "|" + VoiceFormat +
                "|" + voiceData.Length +
                "|" + voiceCount +
                "|" + voiceNowIndex +
                "|" + voiceNowPageLength +
                "|" + Convert.ToBase64String(voiceIndexData) +
                "|" + DateTime.Now.ToString("yyyyMMddhhmmss") +
                "|" + TaskId +
                "|" + FootCode);
            List.Add(rString.ToString());
        }

        return List;
    }
}
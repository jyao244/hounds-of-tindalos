namespace H002.Model;

/// <summary>
///     数据状态 0：结束 1：继续接受
///     Terminal wear state  (0: is not worn; 1: is worn)
/// </summary>
public enum DataState
{
    /// <summary>
    ///     继续
    /// </summary>
    Accept = 1,

    /// <summary>
    ///     结束
    /// </summary>
    End = 0
}
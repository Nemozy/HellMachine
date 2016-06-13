using UnityEngine;

public class Formatter
{
    /// <summary>
    /// Time "mm:ss"
    /// </summary>
    public static string timeFormatMMSS(float seconds)
    {
        string formatReturn = "{0:00}:{1:00}";

        float sec = seconds % 60;
        formatReturn = string.Format(formatReturn, Mathf.FloorToInt((float)seconds / 60), Mathf.FloorToInt(sec));

        return formatReturn;
    }

    /// <summary>
    /// Time "m:ss"
    /// </summary>
    public static string timeFormatMSS(float seconds)
    {
        string formatReturn = "{0:0}:{1:00}";

        float sec = seconds % 60;
        formatReturn = string.Format(formatReturn, Mathf.Abs(Mathf.FloorToInt((float)seconds / 60)), Mathf.Abs(Mathf.FloorToInt(sec)));

        return formatReturn;
    }
}
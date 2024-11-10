using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ”Šw‚ÌŒvZ
/// </summary>
public class Math : MonoBehaviour
{
    /// <summary>
    /// ‹——£
    /// </summary>
    /// <param name="x"> “G‚Æ–¡•û‚Ì‹——£(X) </param>
    /// <param name="y"> “G‚Æ–¡•û‚Ì‹——£(Y) </param>
    /// <returns></returns>
    public static float Distance(float x, float y)
    {
        return Mathf.Sqrt(Squaring(x) +  Squaring(y));
    }

    /// <summary>
    /// ‚Qæ
    /// </summary>
    /// <param name="num"> ”’l </param>
    /// <returns> ‚Qæ‚µ‚½’l </returns>
    public static float Squaring( float num)
    {
        return num * num;
    }
}

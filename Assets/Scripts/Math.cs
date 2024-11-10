using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���w�̌v�Z
/// </summary>
public class Math : MonoBehaviour
{
    /// <summary>
    /// ����
    /// </summary>
    /// <param name="x"> �G�Ɩ����̋���(X) </param>
    /// <param name="y"> �G�Ɩ����̋���(Y) </param>
    /// <returns></returns>
    public static float Distance(float x, float y)
    {
        return Mathf.Sqrt(Squaring(x) +  Squaring(y));
    }

    /// <summary>
    /// �Q��
    /// </summary>
    /// <param name="num"> ���l </param>
    /// <returns> �Q�悵���l </returns>
    public static float Squaring( float num)
    {
        return num * num;
    }
}

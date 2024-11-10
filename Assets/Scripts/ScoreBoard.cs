using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// スコアボードの全体の操作
/// </summary>
public class ScoreBoard : MonoBehaviour
{
    /// <summary>
    /// 倒された数に応じて点数が増える
    /// </summary>
    /// <param name="downEnemy"> 倒された数 </param>
    public void IndicationScoreBoard(int downEnemy)
    {
        textUGUI.text = "Score : " + downEnemy + "000";
    }

    /// <summary> テキスト </summary>
    [SerializeField] private TextMeshProUGUI textUGUI;
}

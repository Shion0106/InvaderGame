using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// �X�R�A�{�[�h�̑S�̂̑���
/// </summary>
public class ScoreBoard : MonoBehaviour
{
    /// <summary>
    /// �|���ꂽ���ɉ����ē_����������
    /// </summary>
    /// <param name="downEnemy"> �|���ꂽ�� </param>
    public void IndicationScoreBoard(int downEnemy)
    {
        textUGUI.text = "Score : " + downEnemy + "000";
    }

    /// <summary> �e�L�X�g </summary>
    [SerializeField] private TextMeshProUGUI textUGUI;
}

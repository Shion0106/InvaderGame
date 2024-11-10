using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �V�[���h�̏����z�u
/// </summary>
public class ShieldGenerator : MonoBehaviour
{
    /// <summary>
    /// �֐�����x�����Ă�
    /// </summary>
    private void Start()
    {
        IndicationShield();
    }

    /// <summary>
    /// �V�[���h��\��������
    /// </summary>
    private void IndicationShield()
    {
        for(int i = 0; i < MaxIndicationShield; i++)
        {
            GameObject instance = Instantiate(shield, new Vector2(InitShieldPositionX + (ShieldIntervalPositionX * i), 
                InitShieldPositionY), Quaternion.identity,this.transform);
            instance.name = ShieldName;
        }
    }

    /// <summary> �V�[���h�̃Q�[���I�u�W�F�N�g </summary>
    [SerializeField] private GameObject shield;
    /// <summary> Hierarchy�ɂ������O </summary>
    private const string ShieldName = "Shield";
    /// <summary> �V�[���h�o��ő吔 </summary>
    private const int MaxIndicationShield = 5;
    /// <summary> �V�[���h�̈ꖇ�ڂ̏����z�u�ꏊ(X) </summary>
    private const float InitShieldPositionX = -5.0f;
    /// <summary> �V�[���h�̈ꖇ�ڂ̏����z�u�ꏊ(Y) </summary>
    private const float InitShieldPositionY = -2.5f;
    /// <summary> �V�[���h�̉��̊Ԋu </summary>
    private const float ShieldIntervalPositionX = 2.5f;
}

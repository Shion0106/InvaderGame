using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// シールドの初期配置
/// </summary>
public class ShieldGenerator : MonoBehaviour
{
    /// <summary>
    /// 関数を一度だけ呼ぶ
    /// </summary>
    private void Start()
    {
        IndicationShield();
    }

    /// <summary>
    /// シールドを表示させる
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

    /// <summary> シールドのゲームオブジェクト </summary>
    [SerializeField] private GameObject shield;
    /// <summary> Hierarchyにだす名前 </summary>
    private const string ShieldName = "Shield";
    /// <summary> シールド出る最大数 </summary>
    private const int MaxIndicationShield = 5;
    /// <summary> シールドの一枚目の初期配置場所(X) </summary>
    private const float InitShieldPositionX = -5.0f;
    /// <summary> シールドの一枚目の初期配置場所(Y) </summary>
    private const float InitShieldPositionY = -2.5f;
    /// <summary> シールドの横の間隔 </summary>
    private const float ShieldIntervalPositionX = 2.5f;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// シールドのゲームオブジェクト
/// </summary>
public class Shield : MonoBehaviour
{
    /// <summary>
    /// 変数の初期化
    /// </summary>
    private void Start()
    {
        nowHitPoint = MaxHitPoint;
    }

    /// <summary>
    /// 常時処理
    /// </summary>
    private void Update()
    {
        ShieldHitPointIsZero();
    }

    /// <summary>
    /// ヒットポイントが0になった時の処理
    /// </summary>
    private void ShieldHitPointIsZero()
    {
        if(nowHitPoint > 0)
        {
            return;
        }

        Destroy(gameObject);
    }

    /// <summary>
    /// 球に当たった時の処理
    /// </summary>
    /// <param name="collision"> 球 </param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.gameObject.GetComponent<Bullet>())
        {
            return;
        }

        if(GameObject.Find("Stage").GetComponent<Stage>().GetIsAllDownEnemy())
        {
            return;
        }

        if(GameObject.Find("Bullet") == null)
        {
            return;
        }

        nowHitPoint--;

        collision.GetComponent<Bullet>().CallDestroy();
    }

    /// <summary> 現在のヒットポイント数 </summary>
    private int nowHitPoint;
    /// <summary> 最大のヒットポイント数 </summary>
    private const int MaxHitPoint = 5;
}

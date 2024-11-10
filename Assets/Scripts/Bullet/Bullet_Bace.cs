using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// バレットの全般的な動き
/// </summary>
public class Bullet_Bace : MonoBehaviour
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public Bullet_Bace(float speed,Vector2 vector,float destroyPositionY,GameObject gameObject)
    {
        bulletSpeed = speed;
        bulletVector2 = vector;
        this.destroyPositionY = destroyPositionY;
        FigtToGameObject = gameObject;
    }

    /// <summary>
    /// クラスやコンポーネントの挿入
    /// </summary>
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// 移動以外の処理
    /// </summary>
    private void Update()
    {
        BulletScreenOutDestroy();
    }

    /// <summary>
    /// 移動の処理
    /// </summary>
    private void FixedUpdate()
    {
        Move();
    }

    /// <summary>
    /// 移動
    /// </summary>
    private void Move()
    {
        rb2d.MovePosition(rb2d.position + bulletVector2 * (bulletSpeed * Time.fixedDeltaTime));
    }

    /// <summary>
    /// 画面外の処理
    /// </summary>
    private void BulletScreenOutDestroy()
    {
        if (rb2d.position.y > -destroyPositionY || destroyPositionY > rb2d.position.y)
        {
            return;
        }

        Destroy(gameObject);
    }

    //ステータス
    /// <summary> 速度 </summary>
    private float bulletSpeed;

    //移動
    /// <summary> リジッドボディ </summary>
    private Rigidbody2D rb2d;
    /// <summary> ベクトル </summary>
    private Vector2 bulletVector2 = new Vector2(0, 0);

    //敵となるゲームオブジェクト
    private GameObject FigtToGameObject;

    //消滅
    /// <summary> 消滅の高さ </summary>
    private float destroyPositionY;
}

using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

/// <summary>
/// 球全般の処理
/// </summary>
public class Bullet : MonoBehaviour
{
    /// <summary>
    /// クラスやコンポーネントの挿入、関数の実行
    /// </summary>
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// 変数の挿入
    /// </summary>
    private void Start()
    {
        bulletSpeed = 5.0f;
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
        rb2d.MovePosition(rb2d.position + bulletVector2 * ( bulletSpeed * Time.fixedDeltaTime));
    }

    /// <summary>
    /// 呼ばれたら削除
    /// </summary>
    public void CallDestroy()
    {
        if (bulletTag == GameObject.Find("Player").GetComponent<Player>().GetBulletTag())
        {
            GameObject.Find("Player").GetComponent<Player>().BulletDestroy();
        }
        Destroy(this.gameObject);
    }

    /// <summary>
    /// 画面外の処理
    /// </summary>
    public void BulletScreenOutDestroy()
    {
        if(this.transform.position.y > DestroyPositionY || this.transform.position.y < -DestroyPositionY)
        {
            CallDestroy();
        }
    }

    /// <summary>
    /// 撃つ方向を代入
    /// </summary>
    /// <param name="vector"> 撃つ方向 </param>
    public void SetBulletVector2(Vector2 vector)
    {
        bulletVector2 = new Vector2(vector.x,vector.y);
    }

    /// <summary>
    /// 現在のポジションを返す(X)
    /// </summary>
    /// <returns> Xの値 </returns>
    public float GetPositionX()
    {
        return rb2d.position.x;
    }

    /// <summary>
    /// 現在のポジションを返す(Y)
    /// </summary>
    /// <returns> Yの値 </returns>
    public float GetPositionY()
    {
        return rb2d.position.y;
    }

    /// <summary>
    /// バレットのタグの挿入
    /// </summary>
    /// <param name="tag"> タグの名前 </param>
    public void SetBulletTag(string tag)
    {
        bulletTag = tag;
    }

    /// <summary>
    /// バレットのタグを返す
    /// </summary>
    /// <returns> タグの名前 </returns>
    public string GetBulletTag()
    {
        return bulletTag;
    }

    //ステータス
    /// <summary> 速度 </summary>
    private float bulletSpeed;
    /// <summary> タグ </summary>
    private string bulletTag = "None";

    //移動
    /// <summary> リジッドボディ </summary>
    private Rigidbody2D rb2d;
    /// <summary> ベクトル </summary>
    private  Vector2 bulletVector2 = new Vector2(0,0);
    
    //消滅
    /// <summary> 消滅の高さ </summary>
    private const float DestroyPositionY = 6.0f;



    ///////////////////////////////////////////////////////////////////////////
    ///何も使わない、失敗したコード

    /// <summary>
    /// プレイヤーにヒットした時の処理
    /// </summary>
    public void HitPlayerDestroy()
    {

        if (GameObject.Find("Player") == null)
        {
            return;
        }

        if (Math.Distance(rb2d.position.x - GameObject.Find("Player").GetComponent<Player>().GetPositionX(),
            rb2d.position.y - GameObject.Find("Player").GetComponent<Player>().GetPositionY()) > 0.3f)
        {
            return;
        }

        if (bulletTag == GameObject.Find("Player").GetComponent<Player>().GetBulletTag())
        {
            return;
        }

        Destroy(this.gameObject);
    }

    /// <summary>
    /// 敵にヒットした時の処理
    /// </summary>
    public void HitEnemyDestroy()
    {
        Debug.LogWarning("よばれない");
        if (GameObject.Find("Enemy") == null)
        {
            return;
        }

        if (Math.Distance(rb2d.position.x - GameObject.Find("Enemy").GetComponent<Enemy>().GetPositionX(),
            rb2d.position.y - GameObject.Find("Enemy").GetComponent<Enemy>().GetPositionY()) > 0.3f)
        {
            return;
        }

        if (bulletTag == GameObject.Find("Enemy").GetComponent<Enemy>().GetBulletTag())
        {
            return;
        }

        Destroy(this.gameObject);
    }
    private const string PlayerBulletTag = "PlayerBullet";
}

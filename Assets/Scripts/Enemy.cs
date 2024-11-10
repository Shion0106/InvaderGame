using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// エネミーの全般の処理
/// </summary>
public class Enemy : MonoBehaviour
{
    /// <summary>
    /// クラスやコンポーネントの挿入、関数の実行
    /// </summary>
    private void Awake()
    {
        //コンポーネント
        rb2d = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// 変数の挿入
    /// </summary>
    private void Start()
    {
        shootVector = new Vector2(0, -1);
    }

    /// <summary>
    /// 表示運動の処理
    /// </summary>
    private void Update()
    {
        
    }

    /// <summary>
    /// 移動の処理
    /// </summary>
    private void FixedUpdate()
    {
        if(GameObject.Find("Stage").GetComponent<Stage>().GetIsAllDownEnemy())
        {
            return;
        }
        Move();
    }

    /// <summary>
    /// 移動
    /// </summary>
    public void Move()
    {
        rb2d.MovePosition(rb2d.position + movementVector2 * (movementSpeed * Time.fixedDeltaTime));
    }

    /// <summary>
    /// 発射
    /// </summary>
    public void Shoot()
    {
        GameObject instance = Instantiate(bullet, new Vector2( rb2d.position.x , rb2d.position.y + InitSpawnPosition), Quaternion.identity);
        instance.name = "Bullet";
        instance.GetComponent<Bullet>().SetBulletVector2(shootVector);
        instance.GetComponent<Bullet>().SetBulletTag(BulletTag);
    }

    
    /// <summary>
    /// 当たった時
    /// </summary>
    /// <param name="other"> 当たったオブジェクト </param>
    public void OnTriggerEnter2D(Collider2D other)
    {
        //弾の場合
        if(other.gameObject.GetComponent<Bullet>())
        {
            Bullet bullet = other.gameObject.GetComponent<Bullet>();

            if (bullet.GetBulletTag() == BulletTag)
            {
                return;
            }
            
            bullet.CallDestroy();
            GameObject.Find("Stage").GetComponent<Stage>().DownEnemy(weightNumber, heightNumber);
            Destroy(this.gameObject);
        }

        //壁に当たったら
    }

    /// <summary>
    /// ベクトルの方向をセット
    /// </summary>
    /// <param name="vector2"> 方向 </param>
    public void SetMovementVector2(Vector2 vector2)
    {
        movementVector2 = vector2;
    }

    /// <summary>
    /// 動く速さのセット
    /// </summary>
    /// <param name="speed"> 速さ </param>
    public void SetMovementSpeed(float speed)
    {
        movementSpeed = speed;
    }

    /// <summary>
    /// 横と縦の何番目にいるかを保存
    /// </summary>
    /// <param name="weight"> 横 </param>
    /// <param name="height"> 縦 </param>
    public void SetWeightNumberAndHeight(int weight,int height)
    {
        weightNumber = weight;
        heightNumber = height;
    }

    /// <summary>
    /// 横の何番目にいるかを返す
    /// </summary>
    /// <returns> 横 </returns>
    public int GetWeightNumber()
    {
        return weightNumber;
    }

    /// <summary>
    /// 縦の何番目にいるかを返す
    /// </summary>
    /// <returns> 縦 </returns>
    public int GetHeightNumber()
    {
        return heightNumber;
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
    /// バレットのタグを返す
    /// </summary>
    /// <returns> タグの名前 </returns>
    public string GetBulletTag()
    {
        return BulletTag;
    }

    //移動
    /// <summary> リジッドボディ </summary>
    private Rigidbody2D rb2d;
    /// <summary> 動く方向 </summary>
    [SerializeField] private Vector2 movementVector2 = new Vector2(0,0);
    /// <summary> 動く速さ </summary>
    [SerializeField] private float movementSpeed = 0;

    //ゲームオブジェクト
    /// <summary> バレット </summary>
    [SerializeField] private GameObject bullet;
    /// <summary> 撃つ方向 </summary>
    private Vector2 shootVector;
    /// <summary> 最初にスポーンさせる位置 </summary>
    private const float InitSpawnPosition = -0.84f;
    /// <summary> タグ </summary>
    private const string BulletTag = "EnemyBullet";

    /// <summary> 横の何番目にいるか </summary>
    [SerializeField] private int weightNumber = 0;
    /// <summary> 縦の何番目にいるか </summary>
    [SerializeField] private int heightNumber = 0;



    ///////////////////////////////////////////////////////////////////////////
    ///何も使わない、失敗したコード
    
    /// <summary>
    /// ダメージ
    /// </summary>
    private void Damage()
    {
        if (GameObject.Find("Bullet") == null)
        {
            return;
        }

        if (Math.Distance(rb2d.position.x - GameObject.Find("Bullet").GetComponent<Bullet>().GetPositionX(),
            rb2d.position.y - GameObject.Find("Bullet").GetComponent<Bullet>().GetPositionY()) > 0.3f)
        {
            return;
        }

        if (BulletTag == GameObject.Find("Bullet").GetComponent<Bullet>().GetBulletTag())
        {
            return;
        }

        //// 当たった後の行動 ////
        //bullet.GetComponent<Bullet>().DestroyBullet();
        Destroy(this.gameObject);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// プレイヤー全般の処理
/// </summary>
public class Player : MonoBehaviour
{

    /// <summary>
    /// クラスやコンポーネントの挿入、関数の実行
    /// </summary>
    private void Awake()
    {
        //インプットアクションに必要な要素
        playerControl = new PlayerControl();
        playerControl.Enable();

        //コンポーネント
        rb2d = GetComponent<Rigidbody2D>();
        GameOverObject.SetActive(false);
    }

    /// <summary>
    /// 変数の挿入
    /// </summary>
    private void Start()
    {
        HartObjectGenerator = new GameObject[MaxHitPoint];
        for(int i = 0; i < MaxHitPoint; i++)
        {
            nowHitPoint++;
            HartObjectGenerator[i] = Instantiate(HartPrefabObject,
                new Vector2(HartInitPositionX - HartIntervalDistanceX * i, HartInitPositionY),Quaternion.identity);
        }

        plSpeed = 5f;
        shootVector = new Vector2(0, 1);
        nowBulletShoot = 0;
    }

    /// <summary>
    /// 移動以外の処理
    /// </summary>
    private void Update()
    {
        Vector();
        Shoot();
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    private void FixedUpdate()
    {
        Move();
    }

    /// <summary>
    /// ベクトルの保存
    /// </summary>
    private void Vector()
    {
        plVector2 = playerControl.Player.Move.ReadValue<Vector2>();
    }

    /// <summary>
    /// 移動
    /// </summary>
    private void Move()
    {
        rb2d.MovePosition(rb2d.position + plVector2 * ( plSpeed * Time.fixedDeltaTime));
        if(rb2d.position.x > RangeOfMovementX)
        {
            rb2d.position = new Vector2(RangeOfMovementX, rb2d.position.y);
        }

        if(rb2d.position.x < -RangeOfMovementX)
        {
            rb2d.position = new Vector2(-RangeOfMovementX, rb2d.position.y);
        }
    }

    /// <summary>
    /// 球を撃つ
    /// </summary>
    private void Shoot()
    {
        if(nowBulletShoot >= MaxBulletNumber)
        {
            return;
        }

        if(playerControl.Player.Shoot.triggered)
        {
            GameObject instance = Instantiate(bullet, new Vector2(rb2d.position.x, rb2d.position.y + ShootY), Quaternion.identity);
            instance.name = BulletTag;
            instance.GetComponent<Bullet>().SetBulletVector2(shootVector);
            instance.GetComponent<Bullet>().SetBulletTag(BulletTag);
            nowBulletShoot++;
        }
    }

    /// <summary>
    /// 球がなくなった時
    /// </summary>
    public void BulletDestroy()
    {
        nowBulletShoot--;
    }

    /// <summary>
    /// 当たった時の判定
    /// </summary>
    /// <param name="collision"> 当たったオブジェクト </param>
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //弾の場合
        if(collision.gameObject.GetComponent<Bullet>())
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();

            if (bullet.GetBulletTag() == BulletTag)
            {
                return;
            }

            nowHitPoint--;
            Destroy(HartObjectGenerator[nowHitPoint].gameObject);
            GameOver();
            bullet.CallDestroy();
        }
    }

    /// <summary>
    /// ゲームオーバー
    /// </summary>
    public void GameOver()
    {
        if (nowHitPoint > 0)
        {
            return;
        }

        GameOverObject.SetActive(true);
        gameOverText.text = "GameOver";
        Debug.Log("GameOver");
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

    //ステータス
    /// <summary> スピード </summary>
    private float plSpeed;
    /// <summary> 最大のヒットポイント </summary>
    private const int MaxHitPoint = 3;
    /// <summary> 現在のヒットポイント </summary>
    private int nowHitPoint = 0;
    /// <summary> ハートのオブジェクト </summary>
    [SerializeField] private GameObject HartPrefabObject;
    /// <summary> ハートのオブジェクトの管理(ヒットポイントをわかりやすくするためのオブジェクト) </summary>
    private GameObject[] HartObjectGenerator;
    /// <summary> ハートの初期位置X(横並びにし、一番左の位置） </summary>
    private const float HartInitPositionX = 8.0f;
    /// <summary> ハートの初期位置Y(横並びにし、一番左の位置） </summary>
    private const float HartInitPositionY = 4.0f;
    /// <summary> ハートのXの間の距離 </summary>
    private const float HartIntervalDistanceX = 1.5f; 
    

    //移動
    /// <summary> リジッドボディ </summary>
    private Rigidbody2D rb2d;
    /// <summary> ベクトル(向きと１の大きさ）を保存 </summary>
    private Vector2 plVector2;
    /// <summary> 動ける範囲 </summary>
    private const float RangeOfMovementX = 8.0f;

    //インプットアクション(今回はインプットシステムを使った移動方法や球を撃つことを行っています)
    /// <summary> スクリプト </summary>
    private PlayerControl playerControl;

    //球の管理
    /// <summary> 球 </summary>
    [SerializeField] private GameObject bullet;
    /// <summary> 今何発撃っているのか </summary>
    private int nowBulletShoot;
    /// <summary> 弾の最大数 </summary>
    private const int MaxBulletNumber = 3;
    /// <summary> 画面に出る弾の管理 </summary>
    private GameObject[] bulletGenerator;
    /// <summary> プレイヤーからどれぐらい離れるか(Y) </summary>
    private const float ShootY = 0.83f;
    /// <summary> 撃つ方向 </summary>
    private Vector2 shootVector;
    /// <summary> タグ </summary>
    private const string BulletTag = "PlayerBullet";

    /// <summary> ゲームクリアのオブジェクト </summary>
    [SerializeField] private GameObject GameOverObject;
    /// <summary> テキスト(ゲームクリア) </summary>
    [SerializeField]private TextMeshProUGUI gameOverText;

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
    }
}

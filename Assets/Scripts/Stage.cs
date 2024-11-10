using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using static Stage;

/// <summary>
/// ステージ全般の動き
/// </summary>
public class Stage : MonoBehaviour
{
    /// <summary>
    /// 変数の挿入
    /// </summary>
    private void Start()
    {
        enemyControl = new EnemyControl[MaxEnemyWeight, MaxEnemyHeight];
        GameClearObject.SetActive(false);

        mostEnemyLeftNumber = 0;
        mostEnemyRightNumber = MaxEnemyWeight - 1;

        for(int i = 0; i < MaxEnemyWeight; i++)
        {
            for(int j = 0; j < MaxEnemyHeight; j++)
            {
                enemyControl[i, j].isLife = true;
                enemyControl[i, j].position = new Vector2(InitEnemyPositionX + i * IntervalDistanceX, InitEnemyPositionY + j * IntervalDistanceY);
            }
        }

        //仮
        BoneEnemy();
    }

    /// <summary>
    /// フレーム処理
    /// </summary>
    private void Update()
    {
        if(isAllDownEnemy)
        {
            return;
        }
        nowUpDownTime += Time.deltaTime;
        nowEnemyShootTime += Time.deltaTime;
        ChangeEnemyVectorTiming();
        EnemyShoot();
    }

    /// <summary>
    /// 敵のベクトル変更のタイミングをはかる
    /// </summary>
    public void ChangeEnemyVectorTiming()
    {
        if(isAllDownEnemy)
        {
            return;
        }

        bool isWallArival = false;
        //右から下に行くとき
        for (int i = 0; i < MaxEnemyHeight; i++)
        {
            if (enemyControl[mostEnemyRightNumber, i].isLife)
            {
                if (enemyControl[mostEnemyRightNumber, i].enemy.GetPositionX() > MaxRightPosition && enemyMovement == EnemyMovement.DownAfterLeftMove)
                {
                    isWallArival = true;
                    break;
                }
            }
        }
        if (isWallArival)
        {
            EnemyMoveVectorGenerator();
            nowUpDownTime = 0;
            return;
        }

        //下から左に行くとき
        if(nowUpDownTime > TimeUnderMove && enemyMovement == EnemyMovement.LeftMove)
        {
            EnemyMoveVectorGenerator();
            nowUpDownTime = 0;
            return;
        }

        //左から下に行くとき
        for (int i = 0; i < MaxEnemyHeight; i++)
        {
            if (enemyControl[mostEnemyLeftNumber, i].isLife)
            {
                if (enemyControl[mostEnemyLeftNumber, i].enemy.GetPositionX() < MaxLeftPosition && enemyMovement == EnemyMovement.DownAfterRightMove)
                {
                    isWallArival = true;
                    break;
                }
            }
        }
        if(isWallArival)
        {
            EnemyMoveVectorGenerator();
            nowUpDownTime = 0;
            return;
        }
        
        //下から右に行くとき
        if(nowUpDownTime > TimeUnderMove && enemyMovement == EnemyMovement.RightMove)
        {
            EnemyMoveVectorGenerator();
            nowUpDownTime = 0;
            return;
        }
    }

    /// <summary>
    /// 敵の初期配置
    /// </summary>
    private void BoneEnemy()
    {
        for(int i = 0;i < MaxEnemyWeight;i++)
        {
            for(int j = 0;j < MaxEnemyHeight;j++)
            {
                GameObject instance = Instantiate(enemyPrefab.gameObject, enemyControl[i, j].position, Quaternion.identity);
                instance.name = "Enemy";
                enemyControl[i, j].gameObject = instance;
                enemyControl[i, j].enemy = enemyControl[i, j].gameObject.GetComponent<Enemy>();
                enemyControl[i, j].enemy.SetWeightNumberAndHeight(i, j);
            }
        }
        EnemyMoveVectorGenerator();
        EnemyMoveSpeedGenerator();
    }

    /// <summary>
    /// 敵の全体のベクトルの管理
    /// </summary>
    public void EnemyMoveVectorGenerator()
    {
        switch(enemyMovement)
        {
            case EnemyMovement.RightMove:
                ChangeEnemyVector2(new Vector2(1, 0));

                enemyMovement = EnemyMovement.DownAfterLeftMove;
                break;
            case EnemyMovement.DownAfterLeftMove:
                ChangeEnemyVector2(new Vector2(0, -1));

                enemyMovement = EnemyMovement.LeftMove;
                break;
            case EnemyMovement.LeftMove:
                ChangeEnemyVector2(new Vector2(-1, 0));

                enemyMovement = EnemyMovement.DownAfterRightMove;
                break;
            case EnemyMovement.DownAfterRightMove:
                ChangeEnemyVector2(new Vector2(0, -1));

                enemyMovement = EnemyMovement.RightMove;
                break;

            default:
                ChangeEnemyVector2(new Vector2(1, 0));

                enemyMovement = EnemyMovement.DownAfterLeftMove;
                break;
        }
    }

    /// <summary>
    /// 敵のベクトルを変更
    /// </summary>
    /// <param name="vector"> ベクトル </param>
    public void ChangeEnemyVector2(Vector2 vector)
    {
        for(int i = 0; i < MaxEnemyWeight; i++)
        {
            for(int  j= 0; j < MaxEnemyHeight; j++)
            {
                if (enemyControl[i, j].isLife)
                {
                    enemyControl[i, j].enemy.SetMovementVector2(vector);
                }
            }
        }
    }

    /// <summary>
    /// 敵がやられた数に応じて速くする
    /// </summary>
    public void EnemyMoveSpeedGenerator()
    {
        switch(downEnemyNumber)
        {
            case <= 0:
                ChangeMoveSpeed(1.0f);
                break;
            case <= 1 * (MaxEnemyHeight * MaxEnemyWeight) / 4:
                ChangeMoveSpeed(1.5f);
                break;
            case <= 2 * (MaxEnemyHeight * MaxEnemyWeight) / 4:
                ChangeMoveSpeed(2.0f);
                break;
            case <= 3 * (MaxEnemyHeight * MaxEnemyWeight) / 4:
                ChangeMoveSpeed(2.5f);
                break;
            case <= MaxEnemyHeight * MaxEnemyWeight - 1:
                ChangeMoveSpeed(3.0f);
                break;
        }
    }

    /// <summary>
    /// 敵の速さを変更
    /// </summary>
    /// <param name="speed"> 速さ </param>
    public void ChangeMoveSpeed(float speed)
    {
        for (int i = 0; i < MaxEnemyWeight; i++)
        {
            for (int j = 0; j < MaxEnemyHeight; j++)
            {
                if (enemyControl[i, j].isLife)
                {
                    enemyControl[i, j].enemy.SetMovementSpeed(speed);
                }
            }
        }
    }

    /// <summary>
    /// 敵が一体やられたときにおこなう
    /// </summary>
    /// <param name="numberX"> 横の番数 </param>
    /// <param name="numberY"> 縦の番数 </param>
    public void DownEnemy(int numberX,int numberY)
    {

        downEnemyNumber++;

        //やられたときに必要なくなるものをなるにし、フォルズでいないことを管理する
        enemyControl[numberX, numberY].enemy = null;
        enemyControl[numberX, numberY].gameObject = null;
        enemyControl[numberX, numberY].isLife = false;
        enemyControl[numberX, numberY].position = new Vector2(0, 0);

        ////やられた後の変数変動////
        if (downEnemyNumber < MaxEnemyWeight * MaxEnemyHeight)
        {
            MostLeftAllEnemyDown();
            MostRightAllEnemyDown();
            EnemyMoveSpeedGenerator();
        }
        GameObject.Find("Canvas").GetComponent<ScoreBoard>().IndicationScoreBoard(downEnemyNumber);
        if(downEnemyNumber >= MaxEnemyHeight * MaxEnemyWeight)
        {
            GameClear();
            isAllDownEnemy = true;
            return;
        }
    }

    /// <summary>
    /// ゲームクリア
    /// </summary>
    public void GameClear()
    {
        gameClearText.text = "GameClear";
        GameClearObject.SetActive(true);
        Debug.Log("GameClear");
    }

    /// <summary>
    /// 一番左の列の敵が全部やられたか
    /// </summary>
    public void MostLeftAllEnemyDown()
    {
        bool isChange = true;
        for (int i = 0; i < MaxEnemyHeight; i++)
        {
            if (enemyControl[mostEnemyLeftNumber, i].isLife)
            {
                isChange = false;
            }
        }
        if (isChange)
        {
            mostEnemyLeftNumber++;
            MostLeftAllEnemyDown();
        }
    }

    /// <summary>
    /// 一番左の列の敵が全部やられたか
    /// </summary>
    public void MostRightAllEnemyDown()
    {
        bool isChange = true;
        for (int i = 0; i < MaxEnemyHeight; i++)
        {
            if (enemyControl[mostEnemyRightNumber, i].isLife)
            {
                isChange = false;
            }
        }
        if (isChange)
        {
            mostEnemyRightNumber--;
            MostRightAllEnemyDown();
        }
    }

    /// <summary>
    /// 敵が球を撃つ
    /// </summary>
    public void EnemyShoot()
    {
        if(isAllDownEnemy)
        {
            return;
        }

        if(nowEnemyShootTime < TimeIntervalEnemyShot)
        {
            return;
        }
        nowEnemyShootTime = 0;
        bool isShot = false;
        do
        {
            int random = Random.Range(mostEnemyLeftNumber, mostEnemyRightNumber + 1);
            for(int i = 0; i < MaxEnemyHeight; i++)
            {
                if (enemyControl[random,i].isLife)
                {
                    enemyControl[random, i].enemy.Shoot();
                    isShot = true;
                    break;
                }
            }
        } while (!isShot);
    }

    /// <summary>
    /// 全員死んでいるか
    /// </summary>
    /// <returns> 全員死んでいるか </returns>
    public bool GetIsAllDownEnemy()
    {
        return isAllDownEnemy;
    }

    /// <summary>
    /// 一番下にいるエネミーを探す。
    /// </summary>
    public void FindMostUnderEnemy()
    {
        if(isAllDownEnemy)
        {
            return;
        }

        for(int i = 0; i < MaxEnemyHeight; i++)
        {
            for(int j = 0; j < MaxEnemyWeight; j++)
            {
                if (enemyControl[j,i].isLife)
                {
                    mostEnemyUnderWidthNumber = j;
                    mostEnemyUnderHeightNumber = i;
                    break;
                }
            }
        }
    }

    /// <summary>
    /// 敵が下に来た時（一定ライン超えたら)
    /// </summary>
    public void WhenTheEnemyComesDown()
    {
        if(isAllDownEnemy)
        {
            return;
        }

        if (enemyControl[mostEnemyUnderWidthNumber,mostEnemyUnderHeightNumber].gameObject.transform.position.y < -4.0f)
        {
            GameObject.Find("Player").GetComponent<Player>().GameOver();
        }
    }

    /// <summary>
    /// 全体のエネミーコントロール
    /// </summary>
    public struct EnemyControl
    {
        /// <summary> オブジェクト </summary>
        public GameObject gameObject;
        /// <summary> スクリプト(Enemy) </summary>
        public Enemy enemy;
        /// <summary> 生きているかどうか </summary>
        public bool isLife;
        /// <summary> 位置 </summary>
        public Vector2 position;
    }
    /// <summary> 全体のエネミーコントロール </summary>
    public EnemyControl[,] enemyControl;

    /// <summary> 敵の動き </summary>
    public enum EnemyMovement
    {
        /// <summary> 右に進む </summary>
        RightMove,
        /// <summary> 下に進んだ後左に進む </summary>
        DownAfterLeftMove,
        /// <summary> 左に進む </summary>
        LeftMove,
        /// <summary> 下に進んだ後右に進む </summary>
        DownAfterRightMove,
    }
    /// <summary> 敵の動き </summary>
    public EnemyMovement enemyMovement = EnemyMovement.RightMove;

    //敵に関してのもの
    /// <summary> 敵のプレファブオブジェクト </summary>
    [SerializeField] private GameObject enemyPrefab;
    /// <summary> 敵が横に並んでいる数 </summary>
    private const int MaxEnemyWeight = 4;
    /// <summary> 敵が縦に並んでいる数 </summary>
    private const int MaxEnemyHeight = 4;
    /// <summary> 一番最初に生成される(左下)のポジションX </summary>
    private const float InitEnemyPositionX = -8.0f;
    /// <summary> 一番最初に生成される(左下)のポジションY </summary>
    private const float InitEnemyPositionY = 1.5f;
    /// <summary> 一定間隔に離れてる距離(X) </summary>
    private const float IntervalDistanceX = 0.8f;
    /// <summary> 一定間隔に離れてる距離(Y) </summary>
    private const float IntervalDistanceY = 0.8f;

    /// <summary> 敵の一番左の列の番数 </summary>
    private int mostEnemyLeftNumber;
    /// <summary> 敵の一番右の列の番数 </summary>
    private int mostEnemyRightNumber;
    /// <summary> 敵の一番左の列の横の番数 </summary>
    private int mostEnemyUnderWidthNumber;
    /// <summary> 敵の一番右の列の縦の番数 </summary>
    private int mostEnemyUnderHeightNumber;

    /// <summary> 右にいれる最大の位置 </summary>
    private const float MaxRightPosition = 8.0f;
    /// <summary> 左にいれる最大の位置 </summary>
    private const float MaxLeftPosition = -8.0f;
    /// <summary> 下に行く時間 </summary>
    private const float TimeUnderMove = 0.2f;
    /// <summary> 敵の撃つ時間の間隔 </summary>
    private const float TimeIntervalEnemyShot = 1.5f;

    /// <summary> 死んだ数 </summary>
    private int downEnemyNumber = 0;
    /// <summary> 全員死んだ </summary>
    private bool isAllDownEnemy = false;

    /// <summary> 上下のいく時間みてる変数 </summary>
    private float nowUpDownTime;
    /// <summary> 敵が撃つ時間をめてる変数 </summary>
    private float nowEnemyShootTime;


    /// <summary> ゲームクリアのオブジェクト </summary>
    [SerializeField] private GameObject GameClearObject;
    /// <summary> ゲームクリアのオブジェクト </summary>
    [SerializeField] private TextMeshProUGUI gameClearText;
}

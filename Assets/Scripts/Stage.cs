using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using static Stage;

/// <summary>
/// �X�e�[�W�S�ʂ̓���
/// </summary>
public class Stage : MonoBehaviour
{
    /// <summary>
    /// �ϐ��̑}��
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

        //��
        BoneEnemy();
    }

    /// <summary>
    /// �t���[������
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
    /// �G�̃x�N�g���ύX�̃^�C�~���O���͂���
    /// </summary>
    public void ChangeEnemyVectorTiming()
    {
        if(isAllDownEnemy)
        {
            return;
        }

        bool isWallArival = false;
        //�E���牺�ɍs���Ƃ�
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

        //�����獶�ɍs���Ƃ�
        if(nowUpDownTime > TimeUnderMove && enemyMovement == EnemyMovement.LeftMove)
        {
            EnemyMoveVectorGenerator();
            nowUpDownTime = 0;
            return;
        }

        //�����牺�ɍs���Ƃ�
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
        
        //������E�ɍs���Ƃ�
        if(nowUpDownTime > TimeUnderMove && enemyMovement == EnemyMovement.RightMove)
        {
            EnemyMoveVectorGenerator();
            nowUpDownTime = 0;
            return;
        }
    }

    /// <summary>
    /// �G�̏����z�u
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
    /// �G�̑S�̂̃x�N�g���̊Ǘ�
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
    /// �G�̃x�N�g����ύX
    /// </summary>
    /// <param name="vector"> �x�N�g�� </param>
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
    /// �G�����ꂽ���ɉ����đ�������
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
    /// �G�̑�����ύX
    /// </summary>
    /// <param name="speed"> ���� </param>
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
    /// �G����̂��ꂽ�Ƃ��ɂ����Ȃ�
    /// </summary>
    /// <param name="numberX"> ���̔Ԑ� </param>
    /// <param name="numberY"> �c�̔Ԑ� </param>
    public void DownEnemy(int numberX,int numberY)
    {

        downEnemyNumber++;

        //���ꂽ�Ƃ��ɕK�v�Ȃ��Ȃ���̂��Ȃ�ɂ��A�t�H���Y�ł��Ȃ����Ƃ��Ǘ�����
        enemyControl[numberX, numberY].enemy = null;
        enemyControl[numberX, numberY].gameObject = null;
        enemyControl[numberX, numberY].isLife = false;
        enemyControl[numberX, numberY].position = new Vector2(0, 0);

        ////���ꂽ��̕ϐ��ϓ�////
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
    /// �Q�[���N���A
    /// </summary>
    public void GameClear()
    {
        gameClearText.text = "GameClear";
        GameClearObject.SetActive(true);
        Debug.Log("GameClear");
    }

    /// <summary>
    /// ��ԍ��̗�̓G���S�����ꂽ��
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
    /// ��ԍ��̗�̓G���S�����ꂽ��
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
    /// �G����������
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
    /// �S������ł��邩
    /// </summary>
    /// <returns> �S������ł��邩 </returns>
    public bool GetIsAllDownEnemy()
    {
        return isAllDownEnemy;
    }

    /// <summary>
    /// ��ԉ��ɂ���G�l�~�[��T���B
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
    /// �G�����ɗ������i��胉�C����������)
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
    /// �S�̂̃G�l�~�[�R���g���[��
    /// </summary>
    public struct EnemyControl
    {
        /// <summary> �I�u�W�F�N�g </summary>
        public GameObject gameObject;
        /// <summary> �X�N���v�g(Enemy) </summary>
        public Enemy enemy;
        /// <summary> �����Ă��邩�ǂ��� </summary>
        public bool isLife;
        /// <summary> �ʒu </summary>
        public Vector2 position;
    }
    /// <summary> �S�̂̃G�l�~�[�R���g���[�� </summary>
    public EnemyControl[,] enemyControl;

    /// <summary> �G�̓��� </summary>
    public enum EnemyMovement
    {
        /// <summary> �E�ɐi�� </summary>
        RightMove,
        /// <summary> ���ɐi�񂾌㍶�ɐi�� </summary>
        DownAfterLeftMove,
        /// <summary> ���ɐi�� </summary>
        LeftMove,
        /// <summary> ���ɐi�񂾌�E�ɐi�� </summary>
        DownAfterRightMove,
    }
    /// <summary> �G�̓��� </summary>
    public EnemyMovement enemyMovement = EnemyMovement.RightMove;

    //�G�Ɋւ��Ă̂���
    /// <summary> �G�̃v���t�@�u�I�u�W�F�N�g </summary>
    [SerializeField] private GameObject enemyPrefab;
    /// <summary> �G�����ɕ���ł��鐔 </summary>
    private const int MaxEnemyWeight = 4;
    /// <summary> �G���c�ɕ���ł��鐔 </summary>
    private const int MaxEnemyHeight = 4;
    /// <summary> ��ԍŏ��ɐ��������(����)�̃|�W�V����X </summary>
    private const float InitEnemyPositionX = -8.0f;
    /// <summary> ��ԍŏ��ɐ��������(����)�̃|�W�V����Y </summary>
    private const float InitEnemyPositionY = 1.5f;
    /// <summary> ���Ԋu�ɗ���Ă鋗��(X) </summary>
    private const float IntervalDistanceX = 0.8f;
    /// <summary> ���Ԋu�ɗ���Ă鋗��(Y) </summary>
    private const float IntervalDistanceY = 0.8f;

    /// <summary> �G�̈�ԍ��̗�̔Ԑ� </summary>
    private int mostEnemyLeftNumber;
    /// <summary> �G�̈�ԉE�̗�̔Ԑ� </summary>
    private int mostEnemyRightNumber;
    /// <summary> �G�̈�ԍ��̗�̉��̔Ԑ� </summary>
    private int mostEnemyUnderWidthNumber;
    /// <summary> �G�̈�ԉE�̗�̏c�̔Ԑ� </summary>
    private int mostEnemyUnderHeightNumber;

    /// <summary> �E�ɂ����ő�̈ʒu </summary>
    private const float MaxRightPosition = 8.0f;
    /// <summary> ���ɂ����ő�̈ʒu </summary>
    private const float MaxLeftPosition = -8.0f;
    /// <summary> ���ɍs������ </summary>
    private const float TimeUnderMove = 0.2f;
    /// <summary> �G�̌����Ԃ̊Ԋu </summary>
    private const float TimeIntervalEnemyShot = 1.5f;

    /// <summary> ���񂾐� </summary>
    private int downEnemyNumber = 0;
    /// <summary> �S������ </summary>
    private bool isAllDownEnemy = false;

    /// <summary> �㉺�̂������Ԃ݂Ă�ϐ� </summary>
    private float nowUpDownTime;
    /// <summary> �G�������Ԃ��߂Ă�ϐ� </summary>
    private float nowEnemyShootTime;


    /// <summary> �Q�[���N���A�̃I�u�W�F�N�g </summary>
    [SerializeField] private GameObject GameClearObject;
    /// <summary> �Q�[���N���A�̃I�u�W�F�N�g </summary>
    [SerializeField] private TextMeshProUGUI gameClearText;
}

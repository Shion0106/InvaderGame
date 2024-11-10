using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// �v���C���[�S�ʂ̏���
/// </summary>
public class Player : MonoBehaviour
{

    /// <summary>
    /// �N���X��R���|�[�l���g�̑}���A�֐��̎��s
    /// </summary>
    private void Awake()
    {
        //�C���v�b�g�A�N�V�����ɕK�v�ȗv�f
        playerControl = new PlayerControl();
        playerControl.Enable();

        //�R���|�[�l���g
        rb2d = GetComponent<Rigidbody2D>();
        GameOverObject.SetActive(false);
    }

    /// <summary>
    /// �ϐ��̑}��
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
    /// �ړ��ȊO�̏���
    /// </summary>
    private void Update()
    {
        Vector();
        Shoot();
    }

    /// <summary>
    /// �ړ�����
    /// </summary>
    private void FixedUpdate()
    {
        Move();
    }

    /// <summary>
    /// �x�N�g���̕ۑ�
    /// </summary>
    private void Vector()
    {
        plVector2 = playerControl.Player.Move.ReadValue<Vector2>();
    }

    /// <summary>
    /// �ړ�
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
    /// ��������
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
    /// �����Ȃ��Ȃ�����
    /// </summary>
    public void BulletDestroy()
    {
        nowBulletShoot--;
    }

    /// <summary>
    /// �����������̔���
    /// </summary>
    /// <param name="collision"> ���������I�u�W�F�N�g </param>
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //�e�̏ꍇ
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
    /// �Q�[���I�[�o�[
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
    /// ���݂̃|�W�V������Ԃ�(X)
    /// </summary>
    /// <returns> X�̒l </returns>
    public float GetPositionX()
    {
        return rb2d.position.x;
    }

    /// <summary>
    /// ���݂̃|�W�V������Ԃ�(Y)
    /// </summary>
    /// <returns> Y�̒l </returns>
    public float GetPositionY()
    {
        return rb2d.position.y;
    }

    /// <summary>
    /// �o���b�g�̃^�O��Ԃ�
    /// </summary>
    /// <returns> �^�O�̖��O </returns>
    public string GetBulletTag()
    {
        return BulletTag;
    }

    //�X�e�[�^�X
    /// <summary> �X�s�[�h </summary>
    private float plSpeed;
    /// <summary> �ő�̃q�b�g�|�C���g </summary>
    private const int MaxHitPoint = 3;
    /// <summary> ���݂̃q�b�g�|�C���g </summary>
    private int nowHitPoint = 0;
    /// <summary> �n�[�g�̃I�u�W�F�N�g </summary>
    [SerializeField] private GameObject HartPrefabObject;
    /// <summary> �n�[�g�̃I�u�W�F�N�g�̊Ǘ�(�q�b�g�|�C���g���킩��₷�����邽�߂̃I�u�W�F�N�g) </summary>
    private GameObject[] HartObjectGenerator;
    /// <summary> �n�[�g�̏����ʒuX(�����тɂ��A��ԍ��̈ʒu�j </summary>
    private const float HartInitPositionX = 8.0f;
    /// <summary> �n�[�g�̏����ʒuY(�����тɂ��A��ԍ��̈ʒu�j </summary>
    private const float HartInitPositionY = 4.0f;
    /// <summary> �n�[�g��X�̊Ԃ̋��� </summary>
    private const float HartIntervalDistanceX = 1.5f; 
    

    //�ړ�
    /// <summary> ���W�b�h�{�f�B </summary>
    private Rigidbody2D rb2d;
    /// <summary> �x�N�g��(�����ƂP�̑傫���j��ۑ� </summary>
    private Vector2 plVector2;
    /// <summary> ������͈� </summary>
    private const float RangeOfMovementX = 8.0f;

    //�C���v�b�g�A�N�V����(����̓C���v�b�g�V�X�e�����g�����ړ����@�⋅�������Ƃ��s���Ă��܂�)
    /// <summary> �X�N���v�g </summary>
    private PlayerControl playerControl;

    //���̊Ǘ�
    /// <summary> �� </summary>
    [SerializeField] private GameObject bullet;
    /// <summary> �����������Ă���̂� </summary>
    private int nowBulletShoot;
    /// <summary> �e�̍ő吔 </summary>
    private const int MaxBulletNumber = 3;
    /// <summary> ��ʂɏo��e�̊Ǘ� </summary>
    private GameObject[] bulletGenerator;
    /// <summary> �v���C���[����ǂꂮ�炢����邩(Y) </summary>
    private const float ShootY = 0.83f;
    /// <summary> ������ </summary>
    private Vector2 shootVector;
    /// <summary> �^�O </summary>
    private const string BulletTag = "PlayerBullet";

    /// <summary> �Q�[���N���A�̃I�u�W�F�N�g </summary>
    [SerializeField] private GameObject GameOverObject;
    /// <summary> �e�L�X�g(�Q�[���N���A) </summary>
    [SerializeField]private TextMeshProUGUI gameOverText;

    ///////////////////////////////////////////////////////////////////////////
    ///�����g��Ȃ��A���s�����R�[�h

    /// <summary>
    /// �_���[�W
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

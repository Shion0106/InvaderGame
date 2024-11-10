using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// �G�l�~�[�̑S�ʂ̏���
/// </summary>
public class Enemy : MonoBehaviour
{
    /// <summary>
    /// �N���X��R���|�[�l���g�̑}���A�֐��̎��s
    /// </summary>
    private void Awake()
    {
        //�R���|�[�l���g
        rb2d = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// �ϐ��̑}��
    /// </summary>
    private void Start()
    {
        shootVector = new Vector2(0, -1);
    }

    /// <summary>
    /// �\���^���̏���
    /// </summary>
    private void Update()
    {
        
    }

    /// <summary>
    /// �ړ��̏���
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
    /// �ړ�
    /// </summary>
    public void Move()
    {
        rb2d.MovePosition(rb2d.position + movementVector2 * (movementSpeed * Time.fixedDeltaTime));
    }

    /// <summary>
    /// ����
    /// </summary>
    public void Shoot()
    {
        GameObject instance = Instantiate(bullet, new Vector2( rb2d.position.x , rb2d.position.y + InitSpawnPosition), Quaternion.identity);
        instance.name = "Bullet";
        instance.GetComponent<Bullet>().SetBulletVector2(shootVector);
        instance.GetComponent<Bullet>().SetBulletTag(BulletTag);
    }

    
    /// <summary>
    /// ����������
    /// </summary>
    /// <param name="other"> ���������I�u�W�F�N�g </param>
    public void OnTriggerEnter2D(Collider2D other)
    {
        //�e�̏ꍇ
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

        //�ǂɓ���������
    }

    /// <summary>
    /// �x�N�g���̕������Z�b�g
    /// </summary>
    /// <param name="vector2"> ���� </param>
    public void SetMovementVector2(Vector2 vector2)
    {
        movementVector2 = vector2;
    }

    /// <summary>
    /// ���������̃Z�b�g
    /// </summary>
    /// <param name="speed"> ���� </param>
    public void SetMovementSpeed(float speed)
    {
        movementSpeed = speed;
    }

    /// <summary>
    /// ���Əc�̉��Ԗڂɂ��邩��ۑ�
    /// </summary>
    /// <param name="weight"> �� </param>
    /// <param name="height"> �c </param>
    public void SetWeightNumberAndHeight(int weight,int height)
    {
        weightNumber = weight;
        heightNumber = height;
    }

    /// <summary>
    /// ���̉��Ԗڂɂ��邩��Ԃ�
    /// </summary>
    /// <returns> �� </returns>
    public int GetWeightNumber()
    {
        return weightNumber;
    }

    /// <summary>
    /// �c�̉��Ԗڂɂ��邩��Ԃ�
    /// </summary>
    /// <returns> �c </returns>
    public int GetHeightNumber()
    {
        return heightNumber;
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

    //�ړ�
    /// <summary> ���W�b�h�{�f�B </summary>
    private Rigidbody2D rb2d;
    /// <summary> �������� </summary>
    [SerializeField] private Vector2 movementVector2 = new Vector2(0,0);
    /// <summary> �������� </summary>
    [SerializeField] private float movementSpeed = 0;

    //�Q�[���I�u�W�F�N�g
    /// <summary> �o���b�g </summary>
    [SerializeField] private GameObject bullet;
    /// <summary> ������ </summary>
    private Vector2 shootVector;
    /// <summary> �ŏ��ɃX�|�[��������ʒu </summary>
    private const float InitSpawnPosition = -0.84f;
    /// <summary> �^�O </summary>
    private const string BulletTag = "EnemyBullet";

    /// <summary> ���̉��Ԗڂɂ��邩 </summary>
    [SerializeField] private int weightNumber = 0;
    /// <summary> �c�̉��Ԗڂɂ��邩 </summary>
    [SerializeField] private int heightNumber = 0;



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

        //// ����������̍s�� ////
        //bullet.GetComponent<Bullet>().DestroyBullet();
        Destroy(this.gameObject);
    }
}

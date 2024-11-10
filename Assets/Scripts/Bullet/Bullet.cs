using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

/// <summary>
/// ���S�ʂ̏���
/// </summary>
public class Bullet : MonoBehaviour
{
    /// <summary>
    /// �N���X��R���|�[�l���g�̑}���A�֐��̎��s
    /// </summary>
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// �ϐ��̑}��
    /// </summary>
    private void Start()
    {
        bulletSpeed = 5.0f;
    }

    /// <summary>
    /// �ړ��ȊO�̏���
    /// </summary>
    private void Update()
    {
        BulletScreenOutDestroy();
    }

    /// <summary>
    /// �ړ��̏���
    /// </summary>
    private void FixedUpdate()
    {
        Move();
    }

    /// <summary>
    /// �ړ�
    /// </summary>
    private void Move()
    {
        rb2d.MovePosition(rb2d.position + bulletVector2 * ( bulletSpeed * Time.fixedDeltaTime));
    }

    /// <summary>
    /// �Ă΂ꂽ��폜
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
    /// ��ʊO�̏���
    /// </summary>
    public void BulletScreenOutDestroy()
    {
        if(this.transform.position.y > DestroyPositionY || this.transform.position.y < -DestroyPositionY)
        {
            CallDestroy();
        }
    }

    /// <summary>
    /// ����������
    /// </summary>
    /// <param name="vector"> ������ </param>
    public void SetBulletVector2(Vector2 vector)
    {
        bulletVector2 = new Vector2(vector.x,vector.y);
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
    /// �o���b�g�̃^�O�̑}��
    /// </summary>
    /// <param name="tag"> �^�O�̖��O </param>
    public void SetBulletTag(string tag)
    {
        bulletTag = tag;
    }

    /// <summary>
    /// �o���b�g�̃^�O��Ԃ�
    /// </summary>
    /// <returns> �^�O�̖��O </returns>
    public string GetBulletTag()
    {
        return bulletTag;
    }

    //�X�e�[�^�X
    /// <summary> ���x </summary>
    private float bulletSpeed;
    /// <summary> �^�O </summary>
    private string bulletTag = "None";

    //�ړ�
    /// <summary> ���W�b�h�{�f�B </summary>
    private Rigidbody2D rb2d;
    /// <summary> �x�N�g�� </summary>
    private  Vector2 bulletVector2 = new Vector2(0,0);
    
    //����
    /// <summary> ���ł̍��� </summary>
    private const float DestroyPositionY = 6.0f;



    ///////////////////////////////////////////////////////////////////////////
    ///�����g��Ȃ��A���s�����R�[�h

    /// <summary>
    /// �v���C���[�Ƀq�b�g�������̏���
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
    /// �G�Ƀq�b�g�������̏���
    /// </summary>
    public void HitEnemyDestroy()
    {
        Debug.LogWarning("��΂�Ȃ�");
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �o���b�g�̑S�ʓI�ȓ���
/// </summary>
public class Bullet_Bace : MonoBehaviour
{
    /// <summary>
    /// �R���X�g���N�^
    /// </summary>
    public Bullet_Bace(float speed,Vector2 vector,float destroyPositionY,GameObject gameObject)
    {
        bulletSpeed = speed;
        bulletVector2 = vector;
        this.destroyPositionY = destroyPositionY;
        FigtToGameObject = gameObject;
    }

    /// <summary>
    /// �N���X��R���|�[�l���g�̑}��
    /// </summary>
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
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
        rb2d.MovePosition(rb2d.position + bulletVector2 * (bulletSpeed * Time.fixedDeltaTime));
    }

    /// <summary>
    /// ��ʊO�̏���
    /// </summary>
    private void BulletScreenOutDestroy()
    {
        if (rb2d.position.y > -destroyPositionY || destroyPositionY > rb2d.position.y)
        {
            return;
        }

        Destroy(gameObject);
    }

    //�X�e�[�^�X
    /// <summary> ���x </summary>
    private float bulletSpeed;

    //�ړ�
    /// <summary> ���W�b�h�{�f�B </summary>
    private Rigidbody2D rb2d;
    /// <summary> �x�N�g�� </summary>
    private Vector2 bulletVector2 = new Vector2(0, 0);

    //�G�ƂȂ�Q�[���I�u�W�F�N�g
    private GameObject FigtToGameObject;

    //����
    /// <summary> ���ł̍��� </summary>
    private float destroyPositionY;
}

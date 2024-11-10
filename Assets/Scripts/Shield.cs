using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �V�[���h�̃Q�[���I�u�W�F�N�g
/// </summary>
public class Shield : MonoBehaviour
{
    /// <summary>
    /// �ϐ��̏�����
    /// </summary>
    private void Start()
    {
        nowHitPoint = MaxHitPoint;
    }

    /// <summary>
    /// �펞����
    /// </summary>
    private void Update()
    {
        ShieldHitPointIsZero();
    }

    /// <summary>
    /// �q�b�g�|�C���g��0�ɂȂ������̏���
    /// </summary>
    private void ShieldHitPointIsZero()
    {
        if(nowHitPoint > 0)
        {
            return;
        }

        Destroy(gameObject);
    }

    /// <summary>
    /// ���ɓ����������̏���
    /// </summary>
    /// <param name="collision"> �� </param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.gameObject.GetComponent<Bullet>())
        {
            return;
        }

        if(GameObject.Find("Stage").GetComponent<Stage>().GetIsAllDownEnemy())
        {
            return;
        }

        if(GameObject.Find("Bullet") == null)
        {
            return;
        }

        nowHitPoint--;

        collision.GetComponent<Bullet>().CallDestroy();
    }

    /// <summary> ���݂̃q�b�g�|�C���g�� </summary>
    private int nowHitPoint;
    /// <summary> �ő�̃q�b�g�|�C���g�� </summary>
    private const int MaxHitPoint = 5;
}

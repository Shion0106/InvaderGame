using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

/// <summary>
/// �V�[���S�̂ŋ��L������̂�����
/// </summary>
public class Scene : MonoBehaviour
{
    /// <summary>
    /// �ϐ��̑}��
    /// </summary>
    private void Start()
    {
        Application.targetFrameRate = FrameRateSpeed;
        //GameScene.SetActive(false);
    }

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.F))
        //{
        //    GameScene.SetActive(true);
        //}
    }

    //�X�e�[�^�X
    /// <summary>
    /// �ő�t���[�����[�g��
    /// </summary>
    private const int FrameRateSpeed = 60;

    /// <summary>
    /// �Q�[���V�[��
    /// </summary>
    [SerializeField] private GameObject GameScene;
}

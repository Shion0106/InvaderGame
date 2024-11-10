using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

/// <summary>
/// シーン全体で共有するものを書く
/// </summary>
public class Scene : MonoBehaviour
{
    /// <summary>
    /// 変数の挿入
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

    //ステータス
    /// <summary>
    /// 最大フレームレート数
    /// </summary>
    private const int FrameRateSpeed = 60;

    /// <summary>
    /// ゲームシーン
    /// </summary>
    [SerializeField] private GameObject GameScene;
}

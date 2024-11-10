using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーのバレット
/// </summary>
public class PlayerBullet : Bullet_Bace
{
    public PlayerBullet():base(10,new Vector2(0,0),1.3f,GameObject.Find("Enemy"))
    {

    }

    
}

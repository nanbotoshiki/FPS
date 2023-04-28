using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/*
SE関連をひとまずコメントアウト 
単体の音声のみ適応中
 */


public class EnemyController : MonoBehaviour
{
    Transform player;
    /*
    NavMeshAgent nav;
    
    Animator animator;
    AudioSource source;
    */

    //範囲(値=メートル)
    /*
    public float traceDist;
    public float RunRange;
    public float AttackRange;
    */
    public int hp = 10;

    //ゾンビを倒した時にカウントするためにgameManagerを追加
    GameManager gameManager;
    public GameManager GameManager
    {
        get
        {
            if (gameManager == null)
            {
                gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
            }
            return gameManager;
        }
    }
    
//プレイヤー認識用(これもいらんかも、Generatorと一緒に消す
    public void Setplayer(Transform player)
    {
        this.player = player;
    }
   
    //南保追記 hpプロパティ
    public int Hp
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
        }
    }
   
    public virtual void TakeDamage(int damage)
    {
        
    }

}

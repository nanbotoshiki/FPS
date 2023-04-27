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


    NavMeshAgent nav;
    Transform player;
    Animator animator;
    AudioSource source;

    //範囲(値=メートル)
    /*
    public float traceDist;
    public float RunRange;
    public float AttackRange;
    public AudioClip SE1;

    */
    public int hp = 10;

    private Collider leftHandCollider;
    private Collider rightHandCollider;

    private bool isInvincible = false;

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
    //SE
    /*
    public void SE()
    {
        EnemySoundEffect.instance.PlaySE();
    }
    */
   
//プレイヤー認識用
    public void Setplayer(Transform player)
    {
        this.player = player;
    }
   
    
    //倒れる処理//将来的に使うかもしれないもの   
   /* public void Delite(int hp)
    {
        if (hp <= 0)
        {
            animator.SetTrigger("dead");
            
        }    
    }
*/

    

    void Start()
    {
    

   


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
   

    //ダメージ処理、0になったら死亡アニメで3秒で消える
    public virtual void TakeDamage(int damage)
    {
        
    }

}

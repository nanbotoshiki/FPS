using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ArcherController : EnemyController
{
    NavMeshAgent nav;
    Transform player;
    Animator animator;
    AudioSource source;
    EnemyFire ef;

    //範囲(値=メートル)
    public float traceDist = 30.0f;//とりあえずゾンビのプレイヤーを認識する距離20m
    public float RunRange = 15.0f;  //ゾンビが走り始める距離15m
    public float AttackRange = 12.0f;//殴る用の距離
    public AudioClip SE1;



    private bool isInvincible = false;

   
    //SE
    /*
    public void SE()
    {
        EnemySoundEffect.instance.PlaySE();
    }
    */
    public void SE()
    {
        source.PlayOneShot(SE1);
    }

    //プレイヤー認識用
   
    //走る用関数
    public void Run(float dist)
    {
        if (dist < RunRange)
        {
            animator.SetBool("Discover", true);
        }
        else
        {
            animator.SetBool("Discover", false);
        }
    }
    //攻撃モーション用関数
    public void Attack(float dist)
    {
        if (dist < AttackRange)
        {
            animator.SetTrigger("Engage");
            ef.Fire();
        }
        else
        {
            //animator.SetBool("Engage", false);
        }
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
        
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        
        //弓にアタッチしたスクリプトを取り出す
        ef = GameObject.Find("Goblin Necro Bow Quiver").GetComponent<EnemyFire>();

        StartCoroutine(CheckDist());
        StartCoroutine(Arrow());

    }

    IEnumerator CheckDist()
    {
        while (true)
        {
            //1秒間に10回発見判定
            yield return new WaitForSeconds(0.1f);
            if (isInvincible)
            {
                yield break;
            }

            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null)
            {
                transform.LookAt(p.transform);
            }
            float dist =
                Vector3.Distance
                (p.transform.position, transform.position);
            //攻撃中は停止させる、索敵範囲に入りましたか?
            if (dist < AttackRange)
            {
                GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
            else if (dist < traceDist)
            {
                //プレイヤーの位置を目的値に設定
                nav.SetDestination(p.transform.position);
                nav.isStopped = false;
                SE();
                Run(dist);
            }
            else
            {
                nav.isStopped = true;
            }
            //           SE();
        }
    }

    IEnumerator Arrow()
    {
        while (true)
        {
            //2秒間に1回発見判定
            yield return new WaitForSeconds(2.0f);
            if (isInvincible)
            {
                yield break;
            }
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null)
            {
                transform.LookAt(p.transform);
            }
            float dist =
                Vector3.Distance
                (p.transform.position, transform.position);
            if (dist < AttackRange)
            {
                animator.SetTrigger("Engage");

                ef.Fire();
            }

        }
    }
    public override void TakeDamage(int damage)
    {
        
        if (isInvincible)
        {
            return; // 無敵状態ならダメージを受けない、死んだら無敵にする
        }
        base.TakeDamage(damage);
        Hp -= damage;
        if (Hp <= 0)
        {
            animator.SetTrigger("dead");
            isInvincible = true;
            Destroy(gameObject, 3.0f);
            base.GameManager.Count++;
        }
        
    }


}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : EnemyController
{
    NavMeshAgent nav;
    Animator animator;

    //範囲(値=メートル)
    public float traceDist = 30.0f;
    public float RunRange = 10.0f;
    public float AttackRange = 5.0f;

    private Collider leftHandCollider;
    private Collider rightHandCollider;
    

    private bool isInvincible = false;

    //走る用関数
    public void Run(float dist)
    {
        if (dist < RunRange)
        {
            animator.SetBool("Discover", true);
            Attack(dist);
        }
        else
        {
            animator.SetBool("Discover", false);
        }
    }
    //攻撃モーション用関数
    public void Attack(float dist)
    {
        if (isInvincible)
        {
            return;
        }
        if (dist < AttackRange)
        {
            /*
            leftHandCollider.enabled = true;
            rightHandCollider.enabled = true;
            Invoke("ColliderReset", 1.5f);
            */
            animator.SetBool("Engage", true);
        }
        else
        {
            animator.SetBool("Engage", false);
        }
    }
    
    void Start()
    {
        
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>();
        

        //敵の攻撃の当たり判定を取得
        
        leftHandCollider = GameObject.Find("Base HumanLArmPalm").GetComponent<CapsuleCollider>();
        rightHandCollider = GameObject.Find("Base HumanRArmPalm").GetComponent<CapsuleCollider>();
        
        //Setplayer(player);

        StartCoroutine(CheckDist());

    }

    IEnumerator CheckDist()
    {
        while (true)
        {
            //1秒間に10回発見判定
            yield return new WaitForSeconds(0.1f);
            if (isInvincible)
            {
                {
                    leftHandCollider.enabled = false;
                }
                if (rightHandCollider != null)
                {
                    rightHandCollider.enabled = false;
                }
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
            if (dist < traceDist)
            {
               
                nav.SetDestination(p.transform.position);
                nav.isStopped = false;
                Run(dist);
            }
            else
            {
                nav.isStopped = true;
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
            SoundManager.instance.Play("ゾンビ死亡");
            animator.SetTrigger("dead");
            animator.SetBool("isDead", true);
            isInvincible = true;
            Destroy(gameObject, 3.0f);
            base.GameManager.Count++;
        }

    }

}
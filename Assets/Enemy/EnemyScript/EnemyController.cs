using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    Transform player;
    Animator animator;

    public void Setplayer(Transform player)
    {
        this.player = player;
    }

    //索敵範囲(値=メートル)
    public float traceDist = 20.0f;//とりあえずゾンビのプレイヤーを認識する距離20m
    public float RunRange = 15.0f;  //ゾンビが走り始める距離
    public float AttackRange = 1.0f;//殴る用の距離
    NavMeshAgent nav;

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        StartCoroutine(CheckDist());
    }

    IEnumerator CheckDist()
    {
        while(true)
        {
            //1秒間に10回発見判定
            yield return new WaitForSeconds(0.1f);
            float dist =
                Vector3.Distance
                (player.position, transform.position);
            //索敵範囲に入りましたか?
            if(dist<traceDist)
            {
                //プレイヤーの位置を目的値に設定
                nav.SetDestination(player.position);
                nav.isStopped = false;
                if (dist < RunRange)
                {
                    animator.SetBool("Discover",true);
                }
                else
                {
                    animator.SetBool("Discover",false);
                }
            }
            else
            {
                nav.isStopped = true;
            }
            if (dist < AttackRange)
            {
                animator.SetTrigger("Engage");
            }
        }
    }
}

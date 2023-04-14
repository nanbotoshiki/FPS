using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    //索敵範囲(値=メートル)
    public float traceDist = 20.0f;//とりあえず20m
    NavMeshAgent nav;

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
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
            }
            else
            {
                nav.isStopped = true;
            }
        }
    }
}

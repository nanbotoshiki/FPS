using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{

    public GameObject prehab;
    public Transform player;
    public int limit; //敵の最大数管理してます。現在10体
    int counter = 0;    //ゾンビの数を管理する予定です

//ランダム配置でオブジェクトに重ならないように改造中。
/*
    base_Eneymy_Position = (10,0);

    Spawn_Position_Rotation = 
    Quaternion.Eular(0,Random.Range(0,180),0) * base_Enemy_Position;

    enemy_Spawn_Position = player.position + Spawn_Position_Rotation;
    

 */

    IEnumerator Start()
    {
        while (true)
        {
            //1秒ごとに1回回ってます。
            yield return new WaitForSeconds(1.0f);
            if (counter < limit)
            {

                GameObject enemy = Instantiate(
                //第一引数、参照するもの
                prehab,
                //第二引数、座標(x,y,z)
                new Vector3(
                    Random.Range(-45f, 45f),
                    Random.Range(1f, 2f),
                    Random.Range(-45f, 45f)
                ),
                //第三引数、向き(デフォルト抜きの場合、Quaternion.identity)
                Quaternion.LookRotation(player.position)
                ) ;

            enemy.GetComponent<EnemyController>().Setplayer(player);
                counter++;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGenerator : MonoBehaviour
{

    public GameObject enemyPrefab;
    public Transform player;
    public float enemySpawnTime;    //敵の出現するまでの時間
    public int enemyLimit;           //敵の最大数管理してます。現在10体
    int enemyCount = 0;            //ゾンビの数を管理する予定です

    IEnumerator Start()
    {
        while (true)
        {
            //指定した秒数毎にインスタンスされます。
            yield return new WaitForSeconds(enemySpawnTime);
            if (enemyCount < enemyLimit)
            {
                float x = Random.Range(-45f, 45f);
                float y = Random.Range(1f, 2f);
                float z = Random.Range(-45f, 45f);
                Vector3 spwonPoint = new Vector3(x, y, z);
                //navMesh.Hit関数はベイクエリアに置ける場合はそのまま
                //置けない場合は、一番近いベイクエリアに代入されるらしい(正直どんなのが裏で動いてるかわからん)
                if (NavMesh.SamplePosition(spwonPoint, out NavMeshHit navMeshHit, 10.0f, NavMesh.AllAreas))
                {
                    GameObject enemy =
                    Instantiate(enemyPrefab, navMeshHit.position, Quaternion.LookRotation(player.position));
                    enemy.GetComponent<EnemyController>().Setplayer(player);
                    enemyCount++;
                }
            }
        }
    }
}

//以前の生成コード
/*              GameObject enemy = Instantiate(
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
*/
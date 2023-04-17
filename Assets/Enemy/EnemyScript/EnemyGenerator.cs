using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{

    public GameObject prehab;
    public Transform player;
    public int limit; //敵の最大数管理してます。現在10体
    int counter = 0;    //ゾンビの数を管理する予定です

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
            Quaternion.identity
            );
            enemy.GetComponent<EnemyController>().Setplayer(player);
                counter++;
            }
        }
    }
}

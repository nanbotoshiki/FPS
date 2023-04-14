using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject prehab;
     IEnumerator Start()
     {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            Instantiate(
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
        }
    }

}

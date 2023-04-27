using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    public GameObject Arrow;
    public float speed;


    public void Fire()
    {
        Invoke("Fire2", 0.5f);
     
    }

    void Fire2()
    {
        GameObject arrow = Instantiate(Arrow, transform.position, Quaternion.Euler(90, 0, 0));

        Rigidbody arrowRb = arrow.GetComponent<Rigidbody>();

        // ミサイルを飛ばす方向を決める。「forward」は「z軸」方向をさす（ポイント）
        arrowRb.AddForce(transform.forward * speed);

        // ３秒後に敵のミサイルを削除する。
        Destroy(arrow, 3.0f);
    }

    
}

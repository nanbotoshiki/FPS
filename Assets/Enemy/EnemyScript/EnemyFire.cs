using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    public GameObject Arrow;
    public float speed;
    public bool rotateTowardsPlayer = true;

    private Quaternion initialRotation;
    void Start()
    {
        // 矢の初期の向きを保存しておく
        initialRotation = Arrow.transform.rotation;
    }
    public void Fire()
    {
        Invoke("Fire2", 0.5f);
     
    }

    void Fire2()
    {
        GameObject arrow = Instantiate(Arrow, transform.position, Quaternion.identity);
        Rigidbody arrowRb = arrow.GetComponent<Rigidbody>();

        // 飛ばす方向を決める。「forward」は「z軸」方向をさす（ポイント）
        arrowRb.AddForce(transform.forward * speed);

        
            arrow.transform.rotation = initialRotation;
        

        // ３秒後に削除する。
        Destroy(arrow, 3.0f);
    }

    
}

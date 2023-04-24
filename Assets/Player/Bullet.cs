using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bloodEffect;
    public GameObject decalHitWall;
    // 弾のエフェクト
    //public GameObject effect;

    // 弾の当たり判定処理
    void OnCollisionEnter(Collision collision)
    {
        // 衝突したオブジェクトが敵である場合
        if (collision.gameObject.tag == "Enemy")
        {
            // 敵を破壊する
            Destroy(collision.gameObject);
            Destroy(gameObject);
            // エフェクトを出す
            Instantiate(bloodEffect, transform.position, Quaternion.identity);
        }
        else
        {
            // 弾を消す
            Destroy(gameObject);
            // 敵以外でも消えるエフェクトを出す　
            Instantiate(decalHitWall, transform.position, Quaternion.LookRotation(collision.contacts[0].normal));
        }
        
    }

    // 弾の移動処理
    void Update()
    {
        
    }
}

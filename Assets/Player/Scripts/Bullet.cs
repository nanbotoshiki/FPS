using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bloodEffect;
    public GameObject decalHitWall;
    Shooter ss;

    GameManager gameManager;
    GameManager GameManager
    {
        get
        {
            if (gameManager == null)
            {
                gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
            }
            return gameManager;
        }
    }
    // 弾のエフェクト
    //public GameObject effect;

    void Start()
    {
        ss = GameObject.Find("Shooter").GetComponent<Shooter>();
    }

    // 弾の当たり判定処理
    void OnCollisionEnter(Collision collision)
    {
        // 衝突したオブジェクトが敵である場合
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyController ec = collision.gameObject.GetComponent<EnemyController>();
            if (ec != null)
            {
                ec.TakeDamage(10);
            }
            Destroy(gameObject);
            GameManager.Count++;
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


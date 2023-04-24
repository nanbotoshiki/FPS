using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameManager gameManager;
    // 弾のエフェクト
    //public GameObject effect;
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

    // 弾の当たり判定処理
    void OnCollisionEnter(Collision collision)
    {
        // 衝突したオブジェクトが敵である場合
        if (collision.gameObject.tag == "Enemy")
        {
            // 敵を破壊する
            Destroy(collision.gameObject);
            GameManager.Count++;

            // エフェクトを出す
            //Instantiate(effect, transform.position, Quaternion.identity);
        }

        // 弾を消す
        Destroy(gameObject);
        // 敵以外でも消えるエフェクトを出す　
        //Instantiate(effect, transform.position, Quaternion.identity);
    }

    // 弾の移動処理
    void Update()
    {
        
    }
}

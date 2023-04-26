using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{

    //オブジェクトと接触した瞬間に呼び出される
    public int damage;

    //コライダーが当たり続けてHPが一瞬でなくならないよう無敵時間を設ける
    private bool isInvincible = false;
    public float invincibleTimer = 2.0f;

    void OnTriggerEnter(Collider other)
    {
        if (isInvincible)
        {
            return; // 無敵状態ならダメージを受けない
        }

        //攻撃した相手がEnemyの場合
        if (other.CompareTag("Player"))
        {
            MyStatus ms = other.gameObject.GetComponent<MyStatus>();
            if (ms != null)
            {
                ms.Hp -= damage;
            }

            isInvincible = true;
            Invoke("ResetInvincibility", invincibleTimer);

        }
    }

    void ResetInvincibility()
    {
        isInvincible = false;
    }

}

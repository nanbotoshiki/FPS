using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellItem : MonoBehaviour
{
    
    Shooter ss;
    private int reward = 20;
    
    void OnCollisionEnter(Collision other)
    {
        ss = GameObject.Find("Shooter").GetComponent<Shooter>();
        if (other.gameObject.tag == "Player")
        {
            // Find()メソッドは、「名前」でオブジェクトを探し特定します。
            // ssオブジェクトを探し出し、それに付いているssスクリプト（component）のデータを取得。
            // 取得したデータを「ss」の箱の中に入れる。
            ss.ShotCount += reward;
            Destroy(gameObject);
            //弾薬取得時のSE予定
            SoundManager.instance.Play("playerアイテム取得");
            if (ss.ShotCount >= ss.MaxShot)
            {
                ss.ShotCount = ss.MaxShot;
            }

            
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellItem : MonoBehaviour
{
    /*
    public AudioClip getSound;
    public GameObject effectPrefab;
    */
    Shooter ss;
    private int reward = 20;

    [SerializeField]
    private SoundManager soundManager; //サウンドマネージャー


    void OnCollisionEnter(Collision other)
    {
        ss = GameObject.Find("Shooter").GetComponent<Shooter>();
        if(other.gameObject.tag == "Player")
        {
            // Find()メソッドは、「名前」でオブジェクトを探し特定します。
            // ssオブジェクトを探し出し、それに付いているssスクリプト（component）のデータを取得。
            // 取得したデータを「ss」の箱の中に入れる。
            ss.ShotCount += reward;
            Destroy(gameObject);
            //弾薬取得時のSE予定
            //soundManager.Play("playerアイテム取得");
            
            /*ちょっと生成前なので忘れた
                        soundManager.Play("playerアイテム取得");
            */
            /*
            AudioSource.PlayClipAtPoint(getSound, Camera.main.transform.position);

            GameObject effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, 0.5f);
            */
        }
    }
    

}

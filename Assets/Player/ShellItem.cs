using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellItem : MonoBehaviour
{
    public AudioClip getSound;
    public GameObject effectPrfab;
    private Shooter ss;
    private int reward = 20;

    /*
    private void OnTriggerEnter(Collider other)
    {
        if(other.gamaObject.tag == "Player")
        {
            // Find()メソッドは、「名前」でオブジェクトを探し特定します。
            // ssオブジェクトを探し出し、それに付いているssスクリプト（component）のデータを取得。
            // 取得したデータを「ss」の箱の中に入れる。
            ss = GameObject.Find("Shooter").GetComponent<Shooter>();
            ss.AddShell(reward);
            Destroy(gameObject);

            AudioSource.PlayClipAtPoint(getSound, Camera.main.transform.position)

            GameObject effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, 0.5f);
        }
    }
    */

}

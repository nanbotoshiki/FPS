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

    
    void OnCollisionEnter(Collision other)
    {
        ss = GameObject.Find("Shooter").GetComponent<Shooter>();
        if(other.gameObject.tag == "Player")
        {
            // Find()���\�b�h�́A�u���O�v�ŃI�u�W�F�N�g��T�����肵�܂��B
            // ss�I�u�W�F�N�g��T���o���A����ɕt���Ă���ss�X�N���v�g�icomponent�j�̃f�[�^���擾�B
            // �擾�����f�[�^���uss�v�̔��̒��ɓ����B
            ss.ShotCount += reward;
            Destroy(gameObject);
            /*
            AudioSource.PlayClipAtPoint(getSound, Camera.main.transform.position);

            GameObject effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, 0.5f);
            */
        }
    }
    

}
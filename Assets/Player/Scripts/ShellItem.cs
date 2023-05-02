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
            // Find()���\�b�h�́A�u���O�v�ŃI�u�W�F�N�g��T�����肵�܂��B
            // ss�I�u�W�F�N�g��T���o���A����ɕt���Ă���ss�X�N���v�g�icomponent�j�̃f�[�^���擾�B
            // �擾�����f�[�^���uss�v�̔��̒��ɓ����B
            ss.ShotCount += reward;
            Destroy(gameObject);
            //�e��擾����SE�\��
            SoundManager.instance.Play("player�A�C�e���擾");
            if (ss.ShotCount >= ss.MaxShot)
            {
                ss.ShotCount = ss.MaxShot;
            }

            
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class MyStatus : MonoBehaviour
{
    const int Defaulthp = 100;
    int hp = Defaulthp;
    Shooter ss;

    public Text bulletText;
    public Text hpText;
   
    public int Hp
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
        }
    }

    bool IsDead()
    {
        return hp <= 0;
    }

    private void Start()
    {
        ss = GameObject.Find("Shooter").GetComponent<Shooter>();
    }

    public void Update()
    {
        hpText.text = Hp.ToString();
        bulletText.text = ss.ShotCount.ToString();

    }

}

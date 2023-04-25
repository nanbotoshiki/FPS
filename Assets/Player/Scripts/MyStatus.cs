using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class MyStatus : MonoBehaviour
{
    const int Defaulthp = 100;
    public int hp = Defaulthp;
    Shooter ss;
    Animator animator;

    public Text bulletText;
    public Text hpText;
    GameManager gm;


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
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        hpText.text = Hp.ToString();
        bulletText.text = ss.ShotCount.ToString();

        if (Hp <= 0)
        {
            enabled = false;
            animator.SetTrigger("death");
            GetComponent<Shooter>().enabled = false;

            gm.GameOver();
        }

    }

}
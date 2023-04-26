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
    GameManager gameManager;

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
        animator = GetComponent<Animator>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    public void Update()
    {
        hpText.text = Hp.ToString();
        bulletText.text = ss.ShotCount.ToString();

        if (Hp <= 0)
        {
            enabled = false;
            animator.SetTrigger("death");
            AimBehaviourBasic aimBehaviour = GetComponent<AimBehaviourBasic>();
            if (aimBehaviour != null)
            {
                aimBehaviour.enabled = false;
            }
            
            Shooter shooter = transform.Find("Shooter").GetComponent<Shooter>();
            if (shooter != null)
            {
                shooter.enabled = false;
            }

            ThirdPersonOrbitCamBasic tps = transform.Find("Main Camera").GetComponent<ThirdPersonOrbitCamBasic>();
            if (tps != null)
            {
                tps.enabled = false;
            }

            Invoke("GameOverDelayed", 2f);
        }

    }

    void GameOverDelayed()
    {
        gameManager.GameOver();
    }

}

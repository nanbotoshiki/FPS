using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    Transform player;
    Animator animator;
    //�v���C���[�F���p
    public void Setplayer(Transform player)
    {
        this.player = player;
    }
    //����p�֐�
    public void Run(float dist)
    {
        if (dist < RunRange)
        {
            animator.SetBool("Discover", true);
            Attack(dist);
        }
        else
        {
            animator.SetBool("Discover", false);
        }
    }
    //�U�����[�V�����p�֐�
    public void Attack(float dist)
    {
        if (dist < AttackRange)
        {
            animator.SetBool("Engage", true);
        }
        else
        {
            animator.SetBool("Engage", false);
        }
    }
    //�|��鏈��//�����I�Ɏg����������Ȃ�����   
   /* public void Delite(int hp)
    {
        if (hp <= 0)
        {
            animator.SetTrigger("dead");
        }    
    }
*/

    //�͈�(�l=���[�g��)
    public float traceDist = 20.0f;//�Ƃ肠�����]���r�̃v���C���[��F�����鋗��20m
    public float RunRange = 15.0f;  //�]���r������n�߂鋗��15m
    public float AttackRange = 5.0f;//����p�̋���

    NavMeshAgent nav;

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        StartCoroutine(CheckDist());
    }

    IEnumerator CheckDist()
    {
        while(true)
        {
            //1�b�Ԃ�10�񔭌�����
            yield return new WaitForSeconds(0.1f);
            float dist =
                Vector3.Distance
                (player.position, transform.position);           
            //���G�͈͂ɓ���܂�����?
            if (dist < traceDist)
            {
                //�v���C���[�̈ʒu��ړI�l�ɐݒ�
                nav.SetDestination(player.position);
                nav.isStopped = false;
                Run(dist);
            }
            else
            {
                nav.isStopped = true;
            }
        }
    }
}
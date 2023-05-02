using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    public GameObject Arrow;
    public float speed;
    public float rotationDuration = 0.1f;


    public void Fire()
    {
        Invoke("Fire2", 0.5f);
    }

    void Fire2()
    {

        GameObject arrow = Instantiate(Arrow, transform.position, Quaternion.identity);
        Rigidbody arrowRb = arrow.GetComponent<Rigidbody>();

        // ��΂����������߂�B�uforward�v�́uz���v�����������i�|�C���g�j
        arrowRb.AddForce(transform.forward * speed);

        // ��̌������v���C���[�̕����ɕύX����
        StartCoroutine(RotateArrowTowardsPlayer(arrow, rotationDuration));

        // �R�b��ɍ폜����B
        Destroy(arrow, 3.0f);
    }
    IEnumerator RotateArrowTowardsPlayer(GameObject arrow, float duration)
    {
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Quaternion startRotation = arrow.transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(playerTransform.position - arrow.transform.position);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            arrow.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        arrow.transform.rotation = targetRotation;
    }

}

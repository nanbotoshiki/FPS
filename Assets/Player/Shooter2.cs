using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shooter2 : MonoBehaviour
{

    public GameObject bulletPrefab;
    public float shotSpeed;
    public static int shotCount; 
    private float shotInterval;


    void Update()
    {
        Animator animator = GetComponentInParent<Animator>();

        if (Input.GetKey(KeyCode.Mouse0) /*&& animator.GetBool("Aim")*/)
        {

            shotInterval += 5 * Time.deltaTime;

            if (shotInterval > 1.0f && shotCount > 0)
            {
                shotCount -= 1;

                GameObject bullet = (GameObject)Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y, 0));
                Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
                bulletRb.AddForce(transform.forward * shotSpeed);
                shotInterval = 0;
                //射撃されてから3秒後に銃弾のオブジェクトを破壊する.

                Destroy(bullet, 3.0f);
            }

        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            shotCount = 30;

        }

    }
}


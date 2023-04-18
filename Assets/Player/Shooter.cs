using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    public GameObject bulletPrefab;
    public float shotSpeed;
    public static int shotCount = 30;
    private float shotInterval;


    void Update()
    {
        Animator animator = GetComponentInParent<Animator>();

        if (Input.GetKey(KeyCode.Mouse0) /*&& animator.GetBool("Aim") == false*/)
        {

            shotInterval += 5 * Time.deltaTime;

            if (shotInterval > 1.0f && shotCount > 0)
            {
                shotCount -= 1;

                GameObject bullet = (GameObject)Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y, 0));
                Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
                bulletRb.AddForce(transform.forward * shotSpeed);
                shotInterval = 0;
                //éÀåÇÇ≥ÇÍÇƒÇ©ÇÁ3ïbå„Ç…èeíeÇÃÉIÉuÉWÉFÉNÉgÇîjâÛÇ∑ÇÈ.

                Destroy(bullet, 3.0f);
            }

        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            shotCount = 30;

        }

    }
}






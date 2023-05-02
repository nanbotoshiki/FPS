using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    public int shotCount = 50;
    private int MaxShotCount = 99;
    public int MaxShot
    {
        get{return MaxShotCount; }
        private set{}
    
    } 
        
    
    public GameObject bulletPrefab;
    public float shotSpeed;
    private float shotInterval;
    public GameObject muzzelSpawn;
    private GameObject holdFlash;
    GameManager gameManager;


    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }


    void Update()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        Animator animator = GetComponentInParent<Animator>();

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (gameManager.isPause)
            {
                return;
            }

            shotInterval += 5 * Time.deltaTime;

            if (shotInterval > 1.0f && shotCount > 0)
            {
                shotCount -= 1;

                if (animator.GetBool("Aim") == false)
                {
                    //射撃エフェクト
                    holdFlash = Instantiate(muzzelSpawn, transform.position, muzzelSpawn.transform.rotation * Quaternion.Euler(0, 0, 90)) as GameObject;
                    //弾を生成
                    GameObject bullet = (GameObject)Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y, 0));
                    Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
                    bulletRb.velocity = transform.forward * shotSpeed;
                    shotInterval = 0;
                    //射撃されてから3秒後に銃弾のオブジェクトを破壊する.

                    Destroy(bullet, 3.0f);

                }
                else if (animator.GetBool("Aim"))
                {
                    //射撃エフェクト
                    holdFlash = Instantiate(muzzelSpawn, transform.position, muzzelSpawn.transform.rotation * Quaternion.Euler(0, 0, 90)) as GameObject;

                    //発射位置とカメラ方向
                    Vector3 startPos = transform.position;
                    Vector3 cameraPosition = Camera.main.transform.forward;
                    //Vector3 shootDirection = (cameraPosition - startPos).normalized;
                    //弾を生成
                    GameObject bullet = Instantiate(bulletPrefab, startPos, Quaternion.identity);
                    Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
                    bulletRb.velocity = cameraPosition * shotSpeed;
                    shotInterval = 0;
                    Destroy(bullet, 3.0f);
                }
                SoundManager.instance.Play("player攻撃");
            }
            else if (ShotCount <= 0)
            {
                SoundManager.instance.Play("player弾切れ");
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            shotInterval = 2.0f;
        }
       
    }


    public int ShotCount
    {
        get
        {
            return shotCount;
        }
        set
        {
            shotCount = value;
        }
    }

}





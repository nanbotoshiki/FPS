using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    public int shotCount = 50;
    const int MaxShotCount = 100;
    public GameObject bulletPrefab;
    public float shotSpeed;
    private float shotInterval;
    public GameObject muzzelSpawn;
    private GameObject holdFlash;
    GameManager gameManager;

    [SerializeField]
    private SoundManager soundManager; //サウンドマネージャー

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
                    //soundManager.Play("player攻撃");
                }
                else if (animator.GetBool("Aim"))
                {
                    /*  完全にカメラの中心から玉を出すスクリプト
                    // カメラの位置と向きを取得する
                    Vector3 cameraPosition = Camera.main.transform.position;
                    Vector3 cameraForward = Camera.main.transform.forward;

                    // 弾の発射位置をカメラの位置に設定する
                    Vector3 startPos = cameraPosition;

                    // カメラの向いている方向に向かって発射する
                    GameObject bullet = Instantiate(bulletPrefab, startPos, Quaternion.identity);
                    Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
                    bulletRb.velocity = cameraForward * shotSpeed;
                    shotInterval = 0;
                    Destroy(bullet, 3.0f);
                    */
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
                    //soundManager.Play("player攻撃");
                }
            }
            else if (ShotCount <= 0)
            {
                //soundManager.Play("player弾切れ");
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            shotInterval = 2.0f;
        }

        /* リロードは4.19に削除（残玉はアイテム取得で回復
        else if (Input.GetKeyDown(KeyCode.R))
        {
            shotCount = 30;
        }
        */
       
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





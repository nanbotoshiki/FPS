using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //アイテム生成
    public GameObject bulletPrefab;
    public float itemSpawnTime;
    GameObject[] bulletArray;
    public float itemLimit;

    //ゾンビ生成
    public GameObject enemyPrefab;
    public Transform player;
    public float enemySpawnTime;    //敵の出現するまでの時間
    public int enemyLimit;           //敵の最大数管理してます。現在10体
    int enemyCount = 0;            //ゾンビの数を管理する予定です



    float countup = 0.0f;
    public Text timeText;
    public Text scoreText;
    public GameObject difficultyButton;
    
    //ゲームクリア時に表示されるキャンバス（常にCanvasを非表示にしておく）
    [SerializeField]
    Canvas gameClearCanvas = null;

    //ゲームオーバー時に表示されるキャンバス（常にCanvasを非表示にしておく）
    [SerializeField]
    Canvas gameOverCanvas = null;

    [SerializeField]
    Canvas Canvas_playerst;

    //敵の撃破数
    [SerializeField]
    Text countText = null;

    //敵の撃破目標
    [SerializeField, Min(1)]
    int maxCount = 5;

    bool isGameClear = false;
    bool isGameOver = false;
    int count = 0;

    public int Count
    {
        set
        {
            count = Mathf.Max(value, 0);
            UpdateCountText();
            if (count >= maxCount)
            {
                GameClear();
            }
        }
        get
        {
            return count;
        }
    }

    //ゾンビの湧き上限をクリア条件に設定する場合↓
    //public GameObject zombie;
    //EnemyGenerator script;
    //int maxCount = script.limit;


    void Start()
    {
        bulletArray = GameObject.FindGameObjectsWithTag("Bullet");
        StartCoroutine("GenerateItem");
        StartCoroutine("GanarateEnemy");

        count = 0;
        
        int def = DifficultyButton.difficulty;

        switch (def)
        {
            case 1:
                maxCount = 5;
                break;
            case 2:
                maxCount = 10;
                break;
            case 3:
                maxCount = 15;
                break;

        }

        Debug.Log(DifficultyButton.difficulty);
        Debug.Log(maxCount);
        UpdateCountText();

    }

    //アイテム生成コルーチン
    IEnumerator GenerateItem()
    {
        while (true)
        {
            bulletArray = GameObject.FindGameObjectsWithTag("Bullet");
            //Debug.Log(bulletArray.Length);
            if (bulletArray.Length < itemLimit)
            {

                NavMeshHit navMeshHit;
                while (true)
                {
                    float x = Random.Range(-50.0f, 50.0f);
                    float z = Random.Range(-47.0f, 45.0f);
                    Vector3 randomPoint = new Vector3(x, 3.0f, z);
                    if (NavMesh.SamplePosition(randomPoint, out navMeshHit, 2.0f, NavMesh.AllAreas))
                    {
                        break;
                    }
                }

                yield return new WaitForSeconds(itemSpawnTime);
                Instantiate(bulletPrefab, navMeshHit.position, Quaternion.identity);


                /*bulletArray = GameObject.FindGameObjectsWithTag("Bullet");
                Debug.Log(bulletArray.Length);

                while (bulletArray.Length >= itemLimit)
                {
                    yield return new WaitForSeconds(1.0f);
                    bulletArray = GameObject.FindGameObjectsWithTag("Bullet");
                    if (bulletArray.Length < itemLimit)
                    {
                        Debug.Log(bulletArray.Length);
                        break;
                    }
                }*/
            }
            yield return null;
        }

    }

    //ゾンビ生成コルーチン
    IEnumerator GanarateEnemy()
    {
        while (true)
        {
            //指定した秒数毎にインスタンスされます。
            yield return new WaitForSeconds(enemySpawnTime);
            if (enemyCount < enemyLimit)
            {
                float x = Random.Range(-45f, 45f);
                float y = Random.Range(1f, 2f);
                float z = Random.Range(-45f, 45f);
                Vector3 spwonPoint = new Vector3(x, y, z);
                //navMesh.Hit関数はベイクエリアに置ける場合はそのまま
                //置けない場合は、一番近いベイクエリアに代入されるらしい(正直どんなのが裏で動いてるかわからん)
                if (NavMesh.SamplePosition(spwonPoint, out NavMeshHit navMeshHit, 10.0f, NavMesh.AllAreas))
                {
                    GameObject enemy =
                    Instantiate(enemyPrefab, navMeshHit.position, Quaternion.LookRotation(player.position));
                    enemy.GetComponent<EnemyController>().Setplayer(player);
                    enemyCount++;
                }
            }
        }
    }

    /*public void StartGame(int def)
    {
        count = 0;
        UpdateCountText();
        
        switch(def)
        {
            case 1:
                maxCount = 5;
                break;
            case 2:
                maxCount = 10;
                break;
            case 3:
                maxCount = 15;
                break;

        }
    }
    */


    void Update()
    {
        //カウントアップの処理
        countup += Time.deltaTime;
        timeText.text = "SCORE" + " " + countup.ToString("f0");
        //クリアorゲームオーバー時に時間を止める
        //if (isGameClear || isGameOver)
        //{
        //  //Debug.Break();
        //    Time.timeScale = 0.0f;
        //    scoreText.text = "Score" + " " + countup.ToString("f0");
            
        //}
    }

    public void GameClear()
    {
        if(isGameClear || isGameOver)
        {

            return;
        }


        //クリア後の処理
        //freezeUpdate = true;
        //ゲームクリアUIの表示
        gameClearCanvas.enabled = true;
        //クリア後にプレイヤーUIを非表示
        Canvas_playerst.enabled = false;
        //カーソル表示させる
        Cursor.lockState = CursorLockMode.None;
        //弾薬の湧き止める


        Time.timeScale = 0.0f;
        scoreText.text = "SCORE" + " " + countup.ToString("f0");

        isGameClear = true;
    }

    public void GameOver()
    {
        if(isGameClear || isGameOver)
        {
            return;
        }

        // ゲームオーバー後の処理
        //freezeUpdate = true;
        //ゲームオーバーUIの表示
        gameOverCanvas.enabled = true;
        //ゲームオーバー後にプレイヤーUIを非表示
        Canvas_playerst.enabled = false;
        //カーソル表示させる
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0.0f;
        isGameOver = true;
    }

    public void Retry()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Stage1");
        
    }

    public void Quit()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Title");
    }
    public void UpdateCountText()
    {
        countText.text = count.ToString() + "/" + maxCount.ToString();
    }
}

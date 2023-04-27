using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    MyStatus script;

    //アイテム生成
    public GameObject bulletPrefab; //アイテムのプレハブ　Asset/Stage/Prefabs/Bullet2(1)
    public float itemSpawnTime;     //アイテムのスポーン時間
    GameObject[] bulletArray;       //生成したアイテムが入る配列
    public float itemLimit;         //アイテムの生成上限

    //ゾンビ生成
    public GameObject enemyPrefab;  //敵のプレハブ　Asset/Enemy/Prehab/Enemy
    public Transform player;        //プレイヤーの位置？？
    public float enemySpawnTime;    //敵の出現するまでの時間
    public int enemyLimit;           //敵の最大数管理してます。現在10体
    int enemyCount = 0;            //ゾンビの数を管理する予定です

    float countup = 0f;           //タイマーの初期設定
    public Text timeText;           //時間表示のテキスト
    public Text scoreText;          //クリア時に表示されるスコアのテキスト
    
    //ゲームクリア時に表示されるキャンバス（常にCanvasを非表示にしておく）
    [SerializeField]
    Canvas gameClearCanvas = null;

    //ゲームオーバー時に表示されるキャンバス（常にCanvasを非表示にしておく）
    [SerializeField]
    Canvas gameOverCanvas = null;

    //ゲームプレイ中に表示するUIのキャンバス
    [SerializeField]
    Canvas Canvas_playerst;

    [SerializeField]
    Canvas Pause;

    [SerializeField]
    Canvas Resume;

    //敵の撃破数
    [SerializeField]
    Text countText = null;

    //敵の撃破目標
    [SerializeField, Min(1)]
    int maxCount = 5;

    //敵の撃破数の初期設定
    int count = 0;

    //ゲーム終了のフラグ
    bool isGameClear = false;
    bool isGameOver = false;

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

        //count = 0;
        
        int def = DifficultyButton.difficulty; //タイトルシーンからdifficultyの数字を取得
        
        script = playerPrefab.GetComponent<MyStatus>();
        //難易度設定項目
        switch (def)
        {
            case 1:　//Easy
                maxCount = 5;
                enemyLimit = 10;
                enemySpawnTime = 5;
                itemLimit = 5;
                itemSpawnTime = 5;
                break;

            case 2: //Normal
                maxCount = 10;
                enemyLimit = 20;
                enemySpawnTime = 4;
                itemLimit = 4;
                itemSpawnTime = 4;
                break;

            case 3: //Hard
                maxCount = 15;
                enemyLimit = 30;
                enemySpawnTime = 3;
                itemLimit = 3;
                itemSpawnTime = 3;
                break;
            case 4: //Hell
                maxCount = 30;
                enemyLimit = 50;
                enemySpawnTime = 1;
                itemLimit = 3;
                itemSpawnTime = 3;
                script.hp = 50;
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


    void Update()
    {
        //カウントアップの処理
        countup += Time.deltaTime;
        timeText.text = "SCORE" + " " + countup.ToString("f0");

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
        //

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

    //Retryボタンを押したとき
    public void Retry()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Stage1");
        
    }

    //Quitボタンを押したとき
    public void Quit()
    {
        DifficultyButton.difficulty = 0;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Title");
    }

    public void PauseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        Pause.enabled = false;
        Resume.enabled = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        Pause.enabled = true;
        Resume.enabled = false;
    }

    //プレイヤーUIの撃破数を更新
    public void UpdateCountText()
    {
        countText.text = count.ToString() + "/" + maxCount.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject playerPrefab;
    MyStatus ms;
    EnemyController ec;


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

    public float countup = 0f;           //タイマーの初期設定
    public Text timeText;           //時間表示のテキスト
    public Text scoreText;          //クリア時に表示されるスコアのテキスト
    
    //ゲームクリア時に表示されるキャンバス（常にCanvasを非表示にしておく）
    [SerializeField]
    Canvas gameClearCanvas = null;

    //ゲームオーバー時に表示されるキャンバス（常にCanvasを非表示にしておく）
    [SerializeField]
    Canvas gameOverCanvas = null;

    //ポーズ画面時のキャンバス（常にCanvasを非表示にしておく）
    [SerializeField]
    Canvas pauseCanvas = null;

    //ゲームプレイ中に表示するUIのキャンバス
    [SerializeField]
    Canvas Canvas_playerst;

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

    public bool isPause = false;

    private Vector3 cursorPos;

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
        //bulletArray = GameObject.FindGameObjectsWithTag("Bullet");
        //count = 0;

        StartCoroutine("GenerateItem");
        StartCoroutine("GanarateEnemy");
        StartCoroutine("CountUpTime");
        StartCoroutine("Pause");


        
        int def = DifficultyButton.difficulty; //タイトルシーンからdifficultyの数字を取得
        
        ms = playerPrefab.GetComponent<MyStatus>(); //プレイヤーのHP取得用
        ec = enemyPrefab.GetComponent<EnemyController>();　//敵のHP取得用
        //難易度設定項目
        switch (def)
        {
            case 1:　//Easy
                maxCount = 5;
                enemyLimit = 10;
                enemySpawnTime = 8;
                itemLimit = 5;
                itemSpawnTime = 5;
                ms.Hp = 100;
                ec.Hp = 10;
                break;

            case 2: //Normal
                maxCount = 10;
                enemyLimit = 20;
                enemySpawnTime = 5;
                itemLimit = 4;
                itemSpawnTime = 5;
                ms.Hp = 100;
                ec.Hp = 20;
                break;

            case 3: //Hard
                maxCount = 15;
                enemyLimit = 30;
                enemySpawnTime = 2;
                itemLimit = 3;
                itemSpawnTime = 10;
                ms.Hp = 50;
                ec.Hp = 30;
                break;

            case 4: //Hell
                maxCount = 30;
                enemyLimit = 50;
                enemySpawnTime = 1;
                itemLimit = 3;
                itemSpawnTime = 20;
                ms.Hp = 50;
                ec.Hp = 40;
                break;
        }

        //Debug.Log(DifficultyButton.difficulty);
        //Debug.Log(maxCount);
        //Debug.Log(ms.Hp);
        UpdateCountText();

    }
   
    //アイテム生成コルーチン
    IEnumerator GenerateItem()
    {
        while (true)
        {
            bulletArray = GameObject.FindGameObjectsWithTag("Bullet");

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
                GameObject obj =
                Instantiate(bulletPrefab, navMeshHit.position, Quaternion.identity);
                //obj.GetComponent<ShellItem>().SetSoundManager(soundManager);

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
                    //enemy.GetComponent<EnemyController>().Setplayer(player);
                    enemyCount++;
                }
            }
        }
    }

    IEnumerator CountUpTime()
    {
        yield return null;
        while (true)
        {
            countup += Time.deltaTime;
            timeText.text = "TIME" + " " + countup.ToString("f0");
            yield return null;

        }
    }

    IEnumerator Pause()
    {
        yield return null;

        while (true)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (isPause == false)
                {
                    PauseGame();
                }
                else
                {
                    ResumeGame();
                }
            }
            yield return null;
        }
    }


    void Update()
    {
        //カウントアップの処理
        //countup += Time.deltaTime;
        //timeText.text = "SCORE" + " " + countup.ToString("f0");

        /*if (Input.GetKeyDown(KeyCode.P))
        {
            if(isPause == false)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }*/
        //Debug.Log(enemyCount);

    }

    public void GameClear()
    {
        if(isGameClear || isGameOver)
        {
            return;
        }

        //クリア後の処理
        gameClearCanvas.enabled = true;         //ゲームクリアUIの表示
        Canvas_playerst.enabled = false;        //クリア後にプレイヤーUIを非表示
        Cursor.lockState = CursorLockMode.None; //カーソル表示させる

        isPause = true;

        Time.timeScale = 0.0f;
        scoreText.text = "SCORE" + " " + countup.ToString("f0");

        isGameClear = true;

        SoundManager.instance.Play("StageClear");
    }

    public void GameOver()
    {
        if(isGameClear || isGameOver)
        {
            return;
        }

        // ゲームオーバー後の処理
        gameOverCanvas.enabled = true;                  //ゲームオーバーUIの表示
        Canvas_playerst.enabled = false;                //ゲームオーバー後にプレイヤーUIを非表示
        Cursor.lockState = CursorLockMode.None;         //カーソル表示させる

        isPause = true;

        Time.timeScale = 0.0f;
        isGameOver = true;

        SoundManager.instance.Play("GameOver");
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
        pauseCanvas.enabled = true;

        isPause = true;

        cursorPos = Input.mousePosition;
    }

    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        pauseCanvas.enabled = false;

        isPause = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.lockState = CursorLockMode.Locked;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        Vector3 delta = Input.mousePosition - cursorPos;
        Camera.main.transform.Rotate(Vector3.up, delta.x);
    }

    //プレイヤーUIの撃破数を更新
    public void UpdateCountText()
    {
        countText.text = count.ToString() + "/" + maxCount.ToString();
    }
}

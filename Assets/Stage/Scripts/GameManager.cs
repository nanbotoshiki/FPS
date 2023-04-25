using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    float countup = 0.0f;
    public Text timeText;
    public Text scoreText;
    
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
        count = 0;
        UpdateCountText();
        //StartCoroutine("CountScore");
    }

    /*IEnumerator CountScore()
    {
        countup += Time.deltaTime;
        timeText.text = "Score" + " " + countup.ToString("f0");

        if()
    }*/

    void Update()
    {
        //カウントアップの処理
        countup += Time.deltaTime;
        timeText.text = "Score" + " " + countup.ToString("f0");
        //クリアorゲームオーバー時に時間を止める
        if (isGameClear || isGameOver)
        {
            Time.timeScale = 0;
            scoreText.text = "Score" + " " + countup.ToString("f0");
            return;
        }
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
         
        isGameOver = true;
    }

    public void Retry()
    {
        SceneManager.LoadScene("Stage1");
    }
    public void UpdateCountText()
    {
        countText.text = count.ToString() + "/" + maxCount.ToString();
    }
}

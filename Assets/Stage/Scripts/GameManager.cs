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
        count = 0;
        UpdateCountText();
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

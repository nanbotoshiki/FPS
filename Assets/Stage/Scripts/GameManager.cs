using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool freezeUpdate = false;
    float countup = 0.0f;
    public Text timeText;

    //[SerializeField]
    //MoveBehaviour moveBehaviour = null;
    
    //ゲームクリア時に表示されるキャンバス（常にCanvasを非表示にしておく）
    [SerializeField]
    Canvas gameClearCanvas = null;

    //ゲームオーバー時に表示されるキャンバス（常にCanvasを非表示にしておく）
    [SerializeField]
    Canvas gameOverCanvas = null;

    //ゲームスコアのUI（秒数）
    [SerializeField]
    Text countText = null;

    //ゲームのクリア条件
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

    // Start is called before the first frame update
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
    // Update is called once per frame
    void Update()
    {
        //カウントアップの処理
        countup += Time.deltaTime;
        timeText.text = "Score" + " " + countup.ToString("f0");
        if (freezeUpdate) return;
      
    }

    public void GameClear()
    {
        if(isGameClear || isGameOver)
        {
            return;
        }


        //クリア後の処理
        freezeUpdate = true;
        //ゲームクリアUIの表示
        gameClearCanvas.enabled = true;
        //カーソル表示させる
        Cursor.lockState = CursorLockMode.None;
        //弾薬の湧き止める
        //プレイヤー止める


        isGameClear = true;
    }

    public void GameOver()
    {
        if(isGameClear || isGameOver)
        {
            return;
        }

        // ゲームオーバー後の処理
        freezeUpdate = true;
        //ゲームオーバーUIの表示
        gameOverCanvas.enabled = true;
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

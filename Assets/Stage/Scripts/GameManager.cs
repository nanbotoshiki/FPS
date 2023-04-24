using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
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
    }

    // Update is called once per frame
    void Update()
    {
        //カウントアップの処理
        countup += Time.deltaTime;
        timeText.text = countup.ToString("f0") + "秒";
        Debug.Log(isGameClear);
      
    }

    public void GameClear()
    {
        if(isGameClear || isGameOver)
        {
            return;
        }


        //クリア後の処理
        gameClearCanvas.enabled = true;
        //弾薬の湧き止める
        //プレイヤー止める
        //moveBehaviour.enabled = false;
       // Cursor.lockState = CursorLockMode.None;

        isGameClear = true;
    }

    public void GameOver()
    {
        if(isGameClear || isGameOver)
        {
            return;
        }
        
         // ゲームオーバー後の処理
         //playerController.enabled = false;
         gameOverCanvas.enabled = true;
         //Cursor.lockState = CursorLockMode.None;
         
        isGameOver = true;
    }

    public void Retry()
    {
        SceneManager.LoadScene(0);
    }
    public void UpdateCountText()
    {
        countText.text = count.ToString() + "/" + maxCount.ToString();
    }
}

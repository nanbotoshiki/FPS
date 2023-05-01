using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Ranking : MonoBehaviour
{
    public GameObject gameManager;
    GameManager gm;

    [SerializeField]
    int point;

    //string[] ranking = { "ランキング1位", "ランキング2位", "ランキング3位", "ランキング4位", "ランキング5位" };

    int dif = DifficultyButton.difficulty -1;

    [SerializeField]
    string[,] ranking = new string[4,5];
    [SerializeField]
    Text[,] rankingText = new Text[4,5];
    [SerializeField]
    float[,] rankingValue = new float[4,5];

    [SerializeField]
    TextArray[] rknm = new TextArray[4];


    // Use this for initialization
    void Start()
    {
        gm = gameManager.GetComponent<GameManager>();

        GetRanking();

        SetRanking(gm.countup);

        /*for (int i = 0; i < rankingText.GetLength(0); i++)
        {
            rankingText[dif,i].text = rankingValue[dif,i].ToString();
        }*/
        for (int i = 0; i < rknm.Length; i++)
        {
            rknm[dif].rkText[i].text = rankingValue[dif,i].ToString();
        }
    }

    /// <summary>
    /// ランキング呼び出し
    /// </summary>
    public void GetRanking()
    {

        //ランキング呼び出し
        for (int i = 0; i < rknm.Length; i++)
        {
            rankingValue[dif,i] = PlayerPrefs.GetInt(ranking[dif,i]);
        }
    }
    /// <summary>
    /// ランキング書き込み
    /// </summary>
    public void SetRanking(float _value)
    {
        //書き込み用
        for (int i = 0; i < ranking.Length; i++)
        {
            //取得した値とRankingの値を比較して入れ替え
            if (_value < rankingValue[dif,i])
            {
                var change = rankingValue[dif,i];
                rankingValue[dif,i] = _value;
                _value = change;
            }
        }

        //入れ替えた値を保存
        for (int i = 0; i < ranking.Length; i++)
        {
            PlayerPrefs.SetFloat(ranking[dif,i], rankingValue[dif,i]);
        }
    }

    [System.Serializable]
    public class TextArray
    {
        public Text[] rkText = new Text[5];
    }
}
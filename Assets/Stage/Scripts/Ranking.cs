using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{
    public GameObject gameManager;
    GameManager gm;

    [SerializeField]
    int point;

    string[] ranking = { "ランキング1位", "ランキング2位", "ランキング3位", "ランキング4位", "ランキング5位" };

    int dif = DifficultyButton.difficulty -1;

    //[SerializeField]
    //string[,] ranking = new string[4,5];
    [SerializeField]
    Text[,] rankingText = new Text[4,5];
    [SerializeField]
    float[,] rankingValue = new float[4,5];


    // Use this for initialization
    void Start()
    {
        gm = gameManager.GetComponent<GameManager>();

        GetRanking();

        SetRanking(gm.countup);

        for (int i = 0; i < rankingText.Length; i++)
        {
            rankingText[dif,i].text = rankingValue[dif,i].ToString();
        }
    }

    /// <summary>
    /// ランキング呼び出し
    /// </summary>
    void GetRanking()
    {

        //ランキング呼び出し
        for (int i = 0; i < ranking.Length; i++)
        {
            rankingValue[dif,i] = PlayerPrefs.GetInt(ranking[i]);
        }
    }
    /// <summary>
    /// ランキング書き込み
    /// </summary>
    void SetRanking(float _value)
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
            PlayerPrefs.SetFloat(ranking[i], rankingValue[dif,i]);
        }
    }
}
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Ranking : MonoBehaviour
{
    public GameObject gameManager;

    int dif = DifficultyButton.difficulty -1;

    [SerializeField]
    int[,] rankingValue = new int[5, 4];

    [SerializeField]
    Text[] rankText = new Text[5];

    //[SerializeField]
    //string[,] ranking = new string[5,4];
    void Start()
    {
    }

    public void GetRanking()
    {
        //�����L���O�Ăяo��
        for (int i = 0; i < 5; i++)
        {
            string key = "HighScore" + i.ToString() + "-" + dif.ToString();
            rankingValue[i,dif] = PlayerPrefs.GetInt(key);
            string rkst = rankingValue[i, dif].ToString();
            
            rankText[i].text = rkst;
        }
    }
    public void SetRanking(int _value)
    {
        //�������ݗp
        for (int i = 0; i < 5; i++)
        {
            //�擾�����l��Ranking�̒l���r���ē���ւ�
            if (_value < rankingValue[i,dif] || rankingValue[i,dif] == 0)
            {
                var change = rankingValue[i,dif];
                rankingValue[i,dif] = _value;
                _value = change;
            }
        }

        //����ւ����l��ۑ�
        for (int i = 0; i < 5; i++)
        {
            string key = "HighScore" + i.ToString() + "-" + dif.ToString();
            PlayerPrefs.SetInt(key, rankingValue[i, dif]);
        }
    }
}
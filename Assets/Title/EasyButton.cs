using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EasyButton : MonoBehaviour
{
    Button button;
    public static int difficulty;
    //public GameManager gameManager;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SetDifficulty);
    }

    void SetDifficulty()
    {
        difficulty = 1;
        SceneManager.LoadScene("Stage1");
        //GameMnager.StartGame(difficulty);
    }


}

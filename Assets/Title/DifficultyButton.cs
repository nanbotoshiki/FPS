using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DifficultyButton : MonoBehaviour
{
    //Button button;
    public static int difficulty;
    //public GameManager gameManager;

    /*private void Awake()
    {
        DontDestroyOnLoad(this);
    }*/
    void Start()
    {
       // button = GetComponent<Button>();
        //button.onClick.AddListener(SetDifficulty);
    }

    public void ButtonClick(string button)
    {
        Debug.Log((string)button);
        switch (button)
        {
            case "Easy":
                difficulty = 1;
                break;
            case "Normal":
                difficulty = 2;
                break;
            case "Hard":
                difficulty = 3;
                break;
        }
    }
    /*void SetDifficulty()
    {
        SceneManager.LoadScene("Stage1");
        //GameMnager.StartGame(difficulty);
    }*/


}

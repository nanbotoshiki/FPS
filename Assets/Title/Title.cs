using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public GameObject easyButton;
    public GameObject normalButton;
    public GameObject hardButton;

    public Canvas soundCanvas;
    public Canvas titleCanvas;

    void Start()
    {
        /*normalButton.gameObject.SetActive(true);
        easyButton.gameObject.SetActive(false);
        hardButton.gameObject.SetActive(false);
        */
    }

    void Update()
    {
        
    }

    public void GameStart()
    {
        if(DifficultyButton.difficulty == 0)
        {
            return;
        }
        SceneManager.LoadScene("Stage1");
    }

    public void SoundButton()
    {
        titleCanvas.enabled = false;
        soundCanvas.enabled = true;
    }

    public void GoToSound()
    {
        titleCanvas.enabled = false;
        soundCanvas.enabled = true;
    }

    public void BackToTitle()
    {
        soundCanvas.enabled = false;
        titleCanvas.enabled = true;
    }
    /*
    public void Right()
    {
        easyButton.gameObject.SetActive(false);
        normalButton.gameObject.SetActive(false);
        hardButton.gameObject.SetActive(true);
    }

    public void Left()
    {
        //normalText.enabled = false;
        //easyText.enabled = true;
    }
    */
}

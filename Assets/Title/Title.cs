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

    // Start is called before the first frame update
    void Start()
    {
        /*normalButton.gameObject.SetActive(true);
        easyButton.gameObject.SetActive(false);
        hardButton.gameObject.SetActive(false);
        */
    }

    // Update is called once per frame
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
}

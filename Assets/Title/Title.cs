using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public Canvas soundCanvas;
    public Canvas titleCanvas;
    [SerializeField]
    private SoundManager soundManager; //サウンドマネージャー

    void Start()
    {

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
        soundManager.Play("ゲームスタート");
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
        soundManager.Play("選択");
    }

    public void BackToTitle()
    {
        soundCanvas.enabled = false;
        titleCanvas.enabled = true;
        soundManager.Play("選択");
    }
}

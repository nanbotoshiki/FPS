using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public Text easyText;
    public Text normalText;
    public Text hardText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameStart()
    {
        SceneManager.LoadScene("Stage1");
    }

    public void Right()
    {
        normalText.enabled = false;
        hardText.enabled = true;
    }

    public void Left()
    {
        normalText.enabled = false;
        easyText.enabled = true;
    }
}

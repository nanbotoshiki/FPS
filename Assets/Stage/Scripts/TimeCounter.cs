using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour
{
    float countup = 0.0f;
    public Text timeText;
    private bool clear = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        countup += Time.deltaTime;
        timeText.text = countup.ToString("f1") + "•b";
    }
}

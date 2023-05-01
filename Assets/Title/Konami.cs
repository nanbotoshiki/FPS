using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Konami : MonoBehaviour
{
    [SerializeField]
    Canvas hiddenButton;

    int cmdSeq = 0;
    int[] keyCodes;
    int[] konamiCommand = new[] {
        (int)KeyCode.UpArrow,
        (int)KeyCode.UpArrow,
        (int)KeyCode.DownArrow,
        (int)KeyCode.DownArrow,
        (int)KeyCode.LeftArrow,
        (int)KeyCode.RightArrow,
        (int)KeyCode.LeftArrow,
        (int)KeyCode.RightArrow,
        (int)KeyCode.B,
        (int)KeyCode.A
    };
    //int kcnt = 0;

    private void Start()
    {
       // keyCodes = (int[])Enum.GetValues(typeof(KeyCode));
    }

    void Update()
    {
        var len = keyCodes.Length;
        for (var i = 0; i < len; i++)
        {
            if (Input.GetKeyUp((KeyCode)keyCodes[i]))
            {
                if (konamiCommand[cmdSeq] == keyCodes[i])
                {
                    cmdSeq++;
                    if (cmdSeq == konamiCommand.Length)
                    {
                        //kcnt++;
                        //print("konami!! " + kcnt);
                        hiddenButton.enabled = true;
                        cmdSeq = 0;
                    }
                }
                else
                {
                    cmdSeq = 0;
                }
            }
        }
    }
}

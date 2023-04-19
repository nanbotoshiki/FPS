using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class MyStatus : MonoBehaviour
{
    private int shotCount = 50;
    public int MaxShotCount = 100;

    public int SetShotCount(int num)
    {
        if(num < MaxShotCount)
        {
            return num;
        }
        else
        {
            return MaxShotCount;
        }
        
    }
    public int GetShotCount()
    {
        return shotCount;
    }
}

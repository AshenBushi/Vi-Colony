using System;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    public void OnPauseClick()
    {
        Debug.Log("Work");
        if (Time.timeScale > 0)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
    
}

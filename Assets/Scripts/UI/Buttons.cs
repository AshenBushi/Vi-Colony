using System;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    [SerializeField] private Button _pause;

    private void OnEnable()
    {
        _pause.onClick.AddListener(OnPauseClick);
    }


    private void OnDisable()
    {
        _pause.onClick.RemoveListener(OnPauseClick);
    }
    
    private void OnPauseClick()
    {
        Debug.Log("Work");
        if (Time.timeScale > 0)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
            
        
    }
    
}

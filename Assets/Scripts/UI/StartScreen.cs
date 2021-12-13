using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class StartScreen : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI personalBestText;
    
    public event Action StartGameEvent;
    
    public event Action OptionsEvent;
    
    public event Action QuitGameEvent;
    
    public event Action EnableEvent;

    public void OnStartGame()
    {
        StartGameEvent?.Invoke();
    }
    public void OnOptions()
    {
        OptionsEvent?.Invoke();
    }
    

    public void OnQuitGame()
    {
        QuitGameEvent?.Invoke();
    }

    private void OnEnable()
    {
        EnableEvent?.Invoke();
    }


    public void SetScoreText(int score)
    {
        
        personalBestText.text= "Personal best : " + score;
    }
}

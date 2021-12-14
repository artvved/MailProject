using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class StartScreen : MonoBehaviour
    {

        [SerializeField] private TextMeshProUGUI personalBestText;
    
        public event Action StartGameEvent;
    
        public event Action OptionsEvent;
        public event Action RulesEvent;
    
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
    
        public void OnRules()
        {
            RulesEvent?.Invoke();
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
}

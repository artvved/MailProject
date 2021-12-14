using System;
using UnityEngine;

namespace UI
{
    public class RulesScreen : MonoBehaviour
    {
    
        public event Action BackToMenuEvent;
    


        public void OnBackToMenu()
        {
            BackToMenuEvent?.Invoke();
        }
   

   







    }
}

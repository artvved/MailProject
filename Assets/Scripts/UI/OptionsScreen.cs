using System;
using UnityEngine;

namespace UI
{
    public class OptionsScreen : MonoBehaviour
    {
    
        public event Action BackToMenuEvent;
        public event Action<float> SoundVolumeChangeEvent;
        public event Action<float> MusicVolumeChangeEvent;


        public void OnBackToMenu()
        {
            BackToMenuEvent?.Invoke();
        }
        public void OnMusicVolumeChange(float vol)
        {
            MusicVolumeChangeEvent?.Invoke(vol);
        }
        public void OnSoundVolumeChange(float vol)
        {
            SoundVolumeChangeEvent?.Invoke(vol);
        }

   







    }
}

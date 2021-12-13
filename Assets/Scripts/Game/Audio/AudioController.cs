using UnityEngine;

namespace Game.Audio
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private AudioSource sound;
        [SerializeField] private AudioSource music;


        public void PlaySound(AudioClip audioClip)
        {
            sound.clip = audioClip;
            sound.Play();
        }
        public void PlayMusic(AudioClip audioClip)
        {
            music.clip = audioClip;
            music.Play();
        }
    }
}
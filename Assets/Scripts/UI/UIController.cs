using System;
using DG.Tweening;
using Game;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private GameController gameController;
        [SerializeField] private StartScreen startScreen;
        [SerializeField] private GameScreen gameScreen;
        [SerializeField] private RulesScreen rulesScreen;
        [SerializeField] private Image deathImage;

        [Header("Options")]
        [SerializeField] private OptionsScreen optionsScreen;

        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider soundSlider;

        private AudioMixerGroup mixer;
        private ColorConverter colorConverter;
        private Vector3 deathImageStartPos;

        private void Start()
        {
            deathImageStartPos = deathImage.rectTransform.position;
            colorConverter = new ColorConverter();
            mixer = gameController.Mixer;
            gameController.DeathEvent += () =>
            {
                deathImage.gameObject.SetActive(true);
                deathImage.rectTransform.DOMoveX(0,0.5f).SetEase(Ease.InOutBack).
                    OnComplete(() =>
                {
                    gameScreen.gameObject.SetActive(false);
                    OpenStartScreenAnimated();
                });
                
            };
            gameController.MoveEvent += () =>
            {
                gameScreen.SetScoreColor(colorConverter.Colors[gameController.PlayerModel.State.GetYColor()]);
            };
            gameController.ScoreChangeEvent += () =>
            {
                gameScreen.SetScoreText(gameController.Score);
            };
            startScreen.EnableEvent += () =>
            {
                startScreen.SetScoreText(gameController.GetPersonalBest());
                gameController.PlayMenuMusic();
            };
            startScreen.StartGameEvent += () =>
            {
                OpenScreen(startScreen.gameObject, gameScreen.gameObject);
                gameController.StartGame();
                gameController.PlayGameMusic();
            };
            startScreen.QuitGameEvent += () => Application.Quit();
            startScreen.OptionsEvent += () => { OpenScreen(startScreen.gameObject,optionsScreen.gameObject);};
            startScreen.RulesEvent += () => { OpenScreen(startScreen.gameObject,rulesScreen.gameObject);};
            optionsScreen.BackToMenuEvent += () =>
            {
                OpenScreen(optionsScreen.gameObject, startScreen.gameObject);
            };
            optionsScreen.MusicVolumeChangeEvent += vol =>
            {
                ChangeMusicVolume(vol);
            };
            optionsScreen.SoundVolumeChangeEvent += vol =>
            {
                ChangeSoundVolume(vol);
            };
            
                musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
                soundSlider.value = PlayerPrefs.GetFloat("SoundVolume", 1f);
            
            rulesScreen.BackToMenuEvent += () =>
            {
                OpenScreen(rulesScreen.gameObject, startScreen.gameObject);
            };
            
            
            startScreen.gameObject.SetActive(true);

        }

        private void OpenStartScreenAnimated()
        {
            startScreen.gameObject.transform.position = deathImageStartPos;
            startScreen.gameObject.SetActive(true);
            startScreen.gameObject.transform.DOMoveX(0,1.5f).SetEase(Ease.InOutBack).
                OnComplete(() =>
                {
                    deathImage.rectTransform.position = deathImageStartPos;
                    deathImage.gameObject.SetActive(false);
                });
            
        }
        
        private void OpenScreen(GameObject oldScreen, GameObject newScreen)
        {
            oldScreen.SetActive(false);
            newScreen.SetActive(true);
        }
        private void ChangeSoundVolume(float volume)
        {
            mixer.audioMixer.SetFloat("SoundVolume", Mathf.Lerp(-80, 0, volume));
            PlayerPrefs.SetFloat("SoundVolume",volume);
        }
    
        private void ChangeMusicVolume(float volume)
        {
            mixer.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0, volume));
            PlayerPrefs.SetFloat("MusicVolume",volume);
        }
    }
}
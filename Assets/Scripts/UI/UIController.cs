using System;
using Game;
using UnityEngine;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private GameController gameController;
        [SerializeField] private StartScreen startScreen;
        [SerializeField] private GameScreen gameScreen;

        private ColorConverter colorConverter;

        private void Start()
        {
            colorConverter = new ColorConverter();
            gameController.DeathEvent += () =>
            {
                OpenScreen(gameScreen.gameObject, startScreen.gameObject);
            };
            gameController.MoveEvent += () =>
            {
                print(gameController.PlayerModel.State.GetYColor());
                gameScreen.SetScoreColor(colorConverter.Colors[gameController.PlayerModel.State.GetYColor()]);
            };
            gameController.ScoreChangeEvent += () =>
            {
                gameScreen.SetScoreText(gameController.Score);
            };
            startScreen.EnableEvent += () =>
            {
                startScreen.SetScoreText(gameController.GetPersonalBest());
            };
            startScreen.StartGameEvent += () =>
            {
                OpenScreen(startScreen.gameObject, gameScreen.gameObject);
                gameController.StartGame();
            };
            startScreen.QuitGameEvent += () => Application.Quit();
           
        }


        private void OpenScreen(GameObject oldScreen, GameObject newScreen)
        {
            oldScreen.SetActive(false);
            newScreen.SetActive(true);
        }
    }
}
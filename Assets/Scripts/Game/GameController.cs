using System;
using Game.Chunk;
using Game.Player;
using UnityEngine;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        [Header("Input")] [SerializeField] private SwipeInputController swipeInputController;
        [Header("Player")] [SerializeField] private PlayerController playerController;
        [SerializeField] private float velocity;
        [Header("Chunk")] [SerializeField] private ChunkManager chunkManager;
        private PlayerModel playerModel;
        private MoveManager moveManager;
        private GameState state;
        private int score = 0;

        public int Score => score;

        public PlayerModel PlayerModel => playerModel;

        public event Action DeathEvent;
        public event Action MoveEvent;
        public event Action ScoreChangeEvent;
        private string personalBestString = "Personal Best";

        void Start()
        {
            state = GameState.DEAD;
            ResetGame();


            swipeInputController.InputEvent += (move =>
            {
                if (state == GameState.ALIVE)
                {
                    moveManager.Move(move);
                    MoveEvent?.Invoke();
                }
            });
            playerController.ObstacleHitEvent += obstacle =>
            {
                var isMatch = playerModel.CheckRequirement(obstacle.DirRequirement, obstacle.ColorRequirement);

                if (isMatch)
                {
                    print("ok");
                }
                else
                {
                    print("ded");
                    StopGame();
                    DeathEvent?.Invoke();
                }
            };

           
        }

        public void StartGame()
        {
            ResetGame();
            state = GameState.ALIVE;
            moveManager.StartSliding();
        }

        private void ResetGame()
        {
            playerController.transform.position = new Vector3(0, 0, 0);
            playerController.transform.rotation = Quaternion.identity;
            playerModel = new PlayerModel(velocity);
            moveManager = new MoveManager(playerModel, playerController, playerController.GetComponent<Rigidbody>());
        }

        public void StopGame()
        {
            state = GameState.DEAD;
            moveManager.StopSliding();
            SaveScore();
        }

        private void SaveScore()
        {
            if (PlayerPrefs.HasKey(personalBestString))
            {
                if (PlayerPrefs.GetInt(personalBestString) < score)
                {
                    PlayerPrefs.SetInt(personalBestString, score);
                }
            }
            else
            {
                PlayerPrefs.SetInt(personalBestString, score);
            }
        }

        public int GetPersonalBest()
        {
            return PlayerPrefs.HasKey(personalBestString) ? PlayerPrefs.GetInt(personalBestString) : 0;
        }


        private void FixedUpdate()
        {
            if (state == GameState.ALIVE)
            {
                score = (int) playerController.transform.position.z;
                ScoreChangeEvent?.Invoke();
                chunkManager.UpdateChunks(playerController.transform);
            }
        }
    }
}
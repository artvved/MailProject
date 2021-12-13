using System;
using Game.Audio;
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
        [SerializeField]private float moveTime ;
        [SerializeField] private float jumpTime;
        [SerializeField]private float jumpForce;
        
        [Header("Chunk")] [SerializeField] private ChunkController chunkController;
        
        [Header("Effects")] [SerializeField] private EffectController effectController;
        
        [Header("Sound")] [SerializeField] private AudioController audioController;
        [SerializeField] private AudioClip rotateSound;
        [SerializeField] private AudioClip jumpSound;
        [SerializeField] private AudioClip matchSound;
        [SerializeField] private AudioClip mismatchSound;
        [SerializeField] private AudioClip gameMusic;
        [SerializeField] private AudioClip menuMusic;

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
                    if (!moveManager.IsMoving)
                    {
                        audioController.PlaySound(moveManager.IsJump(move) ? jumpSound : rotateSound);
                    }

                    moveManager.Move(move);
                    MoveEvent?.Invoke();
                }
            });
           
            playerController.ObstacleHitEvent += obstacle =>
            {
                var isMatch = playerModel.CheckRequirement(obstacle.DirRequirement, obstacle.ColorRequirement);

                if (isMatch)
                {
                    audioController.PlaySound(matchSound);
                    effectController.PlayMatchEffect(playerController.transform.position,obstacle.ColorRequirement);
                }
                else
                {
                    audioController.PlaySound(mismatchSound);
                    effectController.PlayMismatchEffect(playerController.transform.position);
                    StopGame();
                    DeathEvent?.Invoke();
                   
                }
            };

           
        }

        public void StartGame()
        {
            ResetGame();
            state = GameState.ALIVE;
            chunkController.SpawnStartChunks();
            moveManager.StartSliding();
        }

        private void ResetGame()
        {
            playerController.transform.position = new Vector3(0, 0, 0);
            playerController.transform.rotation = Quaternion.identity;
            playerController.gameObject.SetActive(true);
            playerModel = new PlayerModel(moveTime,jumpTime,jumpForce,velocity);
            moveManager = new MoveManager(playerModel, playerController, playerController.GetComponent<Rigidbody>());
           
        }

        private void StopGame()
        {
            state = GameState.DEAD;
            moveManager.StopSliding();
            SaveScore();
            chunkController.ClearChunks();
            playerController.gameObject.SetActive(false);
            
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

        public void PlayMenuMusic()
        {
            audioController.PlayMusic(menuMusic);
        }
        public void PlayGameMusic()
        {
            audioController.PlayMusic(gameMusic);
        }


        private void FixedUpdate()
        {
            if (state == GameState.ALIVE)
            {
                score = (int) playerController.transform.position.z;
                ScoreChangeEvent?.Invoke();
                chunkController.UpdateChunks(playerController.transform);
            }
        }
    }
}
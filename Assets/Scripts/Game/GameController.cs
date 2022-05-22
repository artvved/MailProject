using System;
using Game.Audio;
using Game.Chunk;
using Game.Effects;
using Game.Movement;
using Game.Player;
using UnityEngine;
using UnityEngine.Audio;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        [Header("Input")] [SerializeField] private SwipeInputController swipeInputController;
        
        [Header("Player")] [SerializeField] private PlayerController playerController;
        [SerializeField] private AnimationCurve velocityCurve;
        [SerializeField]private float moveTime ;
        [SerializeField] private float jumpTime;
        [SerializeField]private float jumpForce;
        
        [Header("Chunk")] [SerializeField] private ChunkController chunkController;
        
        [Header("Effects")] [SerializeField] private EffectController effectController;
        [Header("Material Matcher")] [SerializeField] private ColorMaterialMatcher colorMaterialMatcher;
        
        [Header("Sound")] [SerializeField] private AudioController audioController;
        [SerializeField] private AudioClip rotateSound;
        [SerializeField] private AudioClip jumpSound;
        [SerializeField] private AudioClip matchSound;
        [SerializeField] private AudioClip mismatchSound;
        [SerializeField] private AudioClip gameMusic;
        [SerializeField] private AudioClip menuMusic;
        [SerializeField]private AudioMixerGroup mixer;

        private PlayerModel playerModel;
        private MoveManager moveManager;
        private ScoreController scoreController;
        private GameState state;
        private Score score;

        public int Score => score.TotalScore();

        public AudioMixerGroup Mixer => mixer;

        public PlayerModel PlayerModel => playerModel;

        public event Action DeathEvent;
        public event Action MoveEvent;
        public event Action ScoreChangeEvent;
        private string personalBestString = "Personal Best";

       
        void Start()
        {
            scoreController = new ScoreController();
            chunkController.ColorMaterialMatcher = colorMaterialMatcher;
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
                var isMatch = playerModel.CheckRequirement(obstacle.DirectionRequirement, obstacle.ColorRequirement);

                if (isMatch)
                {
                    audioController.PlaySound(matchSound);
                    effectController.PlayMatchEffect(playerController.transform.position,obstacle.ColorRequirement);
                    score.BonusScore += scoreController.PointsForMatch(playerController.transform.position.z);
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
            score = new Score();
            playerController.transform.position = new Vector3(0, 0, 0);
            playerController.transform.rotation = Quaternion.identity;
            playerController.gameObject.SetActive(true);
            playerModel = new PlayerModel(moveTime,jumpTime,jumpForce,velocityCurve);
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
                if (PlayerPrefs.GetInt(personalBestString) < score.TotalScore())
                {
                    PlayerPrefs.SetInt(personalBestString, score.TotalScore());
                }
            }
            else
            {
                PlayerPrefs.SetInt(personalBestString, score.TotalScore());
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
                score.PositionScore = (int) playerController.transform.position.z;
                ScoreChangeEvent?.Invoke();
                chunkController.UpdateChunks(playerController.transform);
            }
        }
    }
}
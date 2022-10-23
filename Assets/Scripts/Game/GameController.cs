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
        [Header("Input")] [SerializeField] private InputController inputController;

        [Header("Player")] [SerializeField] private PlayerView playerView;
        [SerializeField] private AnimationCurve velocityCurve;
        [SerializeField] private float moveTime;
        [SerializeField] private float jumpTime;
        [SerializeField] private float jumpForce;

        [Header("Chunk")] [SerializeField] private ChunkController chunkController;

        [Header("Effects")] [SerializeField] private EffectController effectController;


        [Header("Sound")] [SerializeField] private AudioController audioController;
        [SerializeField] private AudioClip rotateSound;
        [SerializeField] private AudioClip jumpSound;
        [SerializeField] private AudioClip matchSound;
        [SerializeField] private AudioClip mismatchSound;
        [SerializeField] private AudioClip gameMusic;
        [SerializeField] private AudioClip menuMusic;
        [SerializeField] private AudioMixerGroup mixer;

        private PlayerModel playerModel;
        private MoveManager moveManager;
        private ScoreController scoreController;
        private GameState state;
        private Score score;

        public int Score => scoreController.Score.TotalScore();

        public AudioMixerGroup Mixer => mixer;

        public PlayerModel PlayerModel => playerModel;

        public event Action DeathEvent;
        public event Action MoveEvent;
        public event Action ScoreChangeEvent;
        private string personalBestString = "Personal Best";
        private string coinsCountString = "CoinsCount";


        void Start()
        {
            scoreController = new ScoreController();
            chunkController.Init(playerView.transform);
            state = GameState.DEAD;
            ResetGame();

            inputController.InputEvent += (move =>
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
            playerView.CoinHitEvent += coin =>
            {
                Destroy(coin.gameObject);
                score.CoinsCount += 1;
                //play effect
            };
            playerView.ObstacleHitEvent += obstacle =>
            {
                var isMatch = playerModel.CheckRequirement(obstacle.DirectionRequirement, obstacle.ColorRequirement);

                if (isMatch)
                {
                    audioController.PlaySound(matchSound);
                    effectController.PlayMatchEffect(playerView, playerModel.Transform.position,
                        obstacle.ColorRequirement);
                    score.BonusScore += scoreController.PointsForMatch(playerModel.Transform.position.z);
                }
                else
                {
                    audioController.PlaySound(mismatchSound);
                    effectController.PlayMismatchEffect(playerView.transform.position);
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
            scoreController.Score = new Score();
            score = scoreController.Score;

            playerView.transform.position = new Vector3(0, 0, 0);
            playerView.transform.localScale = new Vector3(1, 1, 1);
            playerView.transform.rotation = Quaternion.identity;
            playerView.gameObject.SetActive(true);
            playerModel = new PlayerModel(moveTime, jumpTime, jumpForce, velocityCurve, playerView.transform);
            moveManager = new MoveManager(playerModel, playerModel.Transform, playerView.GetComponent<Rigidbody>());
        }

        private void StopGame()
        {
            state = GameState.DEAD;
            moveManager.StopSliding();
            SaveScore();
            chunkController.ClearChunks();
            playerView.gameObject.SetActive(false);
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
            PlayerPrefs.SetInt(coinsCountString, PlayerPrefs.GetInt(coinsCountString)+score.CoinsCount);

            
        }

        public int GetPersonalBest()
        {
            return PlayerPrefs.HasKey(personalBestString) ? PlayerPrefs.GetInt(personalBestString) : 0;
        }
        public int GetCoinsCount()
        {
            return PlayerPrefs.HasKey(coinsCountString) ? PlayerPrefs.GetInt(coinsCountString) : 0;
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
                score.PositionScore = (int) playerModel.Transform.position.z;
                ScoreChangeEvent?.Invoke();
                chunkController.UpdateChunks();
            }
        }
    }
}
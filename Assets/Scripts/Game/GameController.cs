

using System;
using Game.Chunk;
using Game.Player;
using UnityEngine;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        [Header("Input")]
        [SerializeField] private InputController inputController;
        [Header("Player")]
        [SerializeField] private PlayerController playerController;
        [Header("Chunk")]
        [SerializeField] private ChunkManager chunkManager;
        private PlayerModel playerModel;
        private MoveManager moveManager;
    

        void Start()
        {
            playerModel = new PlayerModel();
            moveManager = new MoveManager(playerModel,playerController.transform);
            inputController.SwipeEvent += (move =>
            {
                moveManager.Move(move);
            });
            playerController.ObstacleHitEvent += obstacle =>
            {
              
                var ok = playerModel.CheckRequirement(obstacle.DirRequirement, obstacle.ColorRequirement);

                if (ok)
                {
                    print("ok");
                }
                else
                {
                    print("dead");
                    //dead
                }
            };
            StartGame();
        }

        private void StartGame()
        {
            playerController.GetComponent<Rigidbody>().velocity = new Vector3(0,0,playerModel.Velocity);
        }


        private void FixedUpdate()
        {
            chunkManager.UpdateChunks(playerController.transform);
        }
    }
}


using System;
using Game.Chunk;
using Game.Player;
using UnityEngine;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private InputController inputController;
        [SerializeField] private PlayerController playerController;
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
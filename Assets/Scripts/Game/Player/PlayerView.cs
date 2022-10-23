using System;

using Game.Obstacles;

using UnityEngine;

namespace Game.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerView : MonoBehaviour
    {
        public event Action<Obstacle> ObstacleHitEvent;
        public event Action<CoinView> CoinHitEvent;
        private void OnTriggerEnter(Collider other)
        {
            var obs = other.GetComponent<Obstacle>();
            if (obs!=null)
            {
                ObstacleHitEvent?.Invoke(obs);
            }
            var coin = other.GetComponent<CoinView>();
            if (coin!=null)
            {
                CoinHitEvent?.Invoke(coin);
            }
            
            
        }
    }
}
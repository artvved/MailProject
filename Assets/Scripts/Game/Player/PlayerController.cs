using System;

using Game.Obstacles;

using UnityEngine;

namespace Game.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        public event Action<Obstacle> ObstacleHitEvent;
        private void OnTriggerEnter(Collider other)
        {
            var obs = other.GetComponent<Obstacle>();
            if (obs!=null)
            {
                ObstacleHitEvent?.Invoke(obs);
            }
                
            
            
        }
    }
}
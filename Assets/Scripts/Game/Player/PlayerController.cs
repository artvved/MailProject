using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Game.Player
{
    
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
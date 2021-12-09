using Game.Player;
using UnityEngine;

namespace Game.Obstacles
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private Direction directionRequirement;
        [SerializeField] Color colorRequirement;
        public Direction DirRequirement => directionRequirement;
        public Color ColorRequirement => colorRequirement;


    }
}

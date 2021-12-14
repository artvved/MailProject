using Game.Player;
using UnityEngine;
using Color = Game.Player.Color;

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

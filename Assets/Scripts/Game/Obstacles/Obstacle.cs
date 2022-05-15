using Game.Player;
using UnityEngine;
using Color = Game.Player.Color;

namespace Game.Obstacles
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private Direction directionRequirement;
        [SerializeField] private Color colorRequirement;
        

        public Direction DirectionRequirement
        {
            get => directionRequirement;
            set => directionRequirement = value;
        }
        
        public Color ColorRequirement
        {
            get => colorRequirement;
            set => colorRequirement = value;
        }
    }
}

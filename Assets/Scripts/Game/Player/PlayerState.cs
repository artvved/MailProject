using System.Collections.Generic;
using Game.Player;

namespace Game
{
    public class PlayerState
    {
        private Dictionary<Direction, Color> directionColorPairs = new Dictionary<Direction, Color>();

        

        public PlayerState()
        {
            directionColorPairs.Add(Direction.X,Color.GREEN);
            directionColorPairs.Add(Direction.Y,Color.PURPULE);
            directionColorPairs.Add(Direction.Z,Color.ORANGE);
        }
        
       

        public void RotateSide()
        {
            (directionColorPairs[Direction.X], directionColorPairs[Direction.Y]) = (directionColorPairs[Direction.Y], directionColorPairs[Direction.X]);
        }
        
        public void RotateBack()
        {
            (directionColorPairs[Direction.Y], directionColorPairs[Direction.Z]) = (directionColorPairs[Direction.Z], directionColorPairs[Direction.Y]);
        }
        
        
        public Color GetXColor()
        {
            return directionColorPairs[Direction.X];
        }
        public Color GetYColor()
        {
            return directionColorPairs[Direction.Y];
        }
        public Color GetZColor()
        {
            return directionColorPairs[Direction.Z];
        }
    }
}
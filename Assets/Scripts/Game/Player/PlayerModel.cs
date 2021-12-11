using UnityEngine;

namespace Game.Player
{
    public class PlayerModel
    {
        private PlayerState state;

        public PlayerState State => state;


        public Vector3 Velocity { get;  }


        public PlayerModel(float velocity)
        {
            state = new PlayerState();
            Velocity = new Vector3(0, 0, velocity);
        }


        public bool CheckRequirement(Direction direction,Color color)
        {
            return state.CheckMatch(direction, color);
        }

        public void Move(Move move)
        {
            switch (move)
            {
                case Game.Move.LEFT:
                    state.RotateSide();
                    break;
                case Game.Move.RIGHT:
                    state.RotateSide();
                    break;
                case Game.Move.BACK:
                    state.RotateBack();
                    break;

            }
          
        }
    }
}
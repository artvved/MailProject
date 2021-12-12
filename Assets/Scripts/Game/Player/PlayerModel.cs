using UnityEngine;

namespace Game.Player
{
    public class PlayerModel
    {
        private PlayerState state;
        private float moveTime = 0.2f;
        private float jumpTime = 0.5f;
        private float jumpForce = 1.5f;
        private Vector3 velocity;
        
        public PlayerState State => state;
        public float MoveTime => moveTime;
        public float JumpTime => jumpTime;
        public float JumpForce => jumpForce;
        public Vector3 Velocity => velocity;

        public PlayerModel( float moveTime, float jumpTime, float jumpForce, float velocity)
        {
            state = new PlayerState();
            this.moveTime = moveTime;
            this.jumpTime = jumpTime;
            this.jumpForce = jumpForce;
            this.velocity =  new Vector3(0, 0, velocity);
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
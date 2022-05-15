using Game.Movement;
using UnityEngine;

namespace Game.Player
{
    public class PlayerModel
    {
        private PlayerState state;
        private float moveTime ;
        private float jumpTime ;
        private float jumpForce ;
        private AnimationCurve velocityCurve;
        
        public PlayerState State => state;
        public float MoveTime => moveTime;
        public float JumpTime => jumpTime;
        public float JumpForce => jumpForce;
        public AnimationCurve VelocityCurve => velocityCurve;

        public PlayerModel( float moveTime, float jumpTime, float jumpForce, AnimationCurve velocityCurve)
        {
            state = new PlayerState();
            this.moveTime = moveTime;
            this.jumpTime = jumpTime;
            this.jumpForce = jumpForce;
            this.velocityCurve = velocityCurve;
        }

        


        public bool CheckRequirement(Direction direction,Color color)
        {
            return state.CheckMatch(direction, color);
        }

        public void Move(Move move)
        {
            switch (move)
            {
                case Movement.Move.LEFT:
                    state.RotateSide();
                    break;
                case Movement.Move.RIGHT:
                    state.RotateSide();
                    break;
                case Movement.Move.BACK:
                    state.RotateBack();
                    break;

            }
          
        }
    }
}
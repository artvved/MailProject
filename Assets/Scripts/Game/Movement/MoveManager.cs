using DG.Tweening;
using Game.Player;
using UnityEngine;

namespace Game.Movement
{
    public class MoveManager
    {
        //player
        private PlayerModel playerModel;
        private Transform playerTransform;
        private AnimationCurve velocityCurve;
        private Rigidbody rigidbody;

        //lines
        private float lineSideDistance = 1.2f;
        private int lineCount = 3;
        private int curLine = 1;


        private bool isMoving = false;

        public bool IsMoving => isMoving;

        public MoveManager(PlayerModel playerModel, Transform transform, Rigidbody rigidbody)
        {
            this.playerModel = playerModel;
            playerTransform = transform;
            velocityCurve = playerModel.VelocityCurve;
            this.rigidbody = rigidbody;
        }

        public void Move(Move move)
        {
            if (!isMoving)
            {
               
                playerModel.Move(move);
                MoveTransform(move);
            }
        }

        private void MoveTransform(Move move)
        {
            StopSliding();
            var dir = ConsiderMovement(move);
            var rot = ConsiderRotation(move);


            isMoving = true;
            playerTransform.DORotate(rot, playerModel.MoveTime, RotateMode.WorldAxisAdd).SetEase(Ease.Linear);
            var velocity = new Vector3(0, 0, 1);
            var z=playerTransform.position.z;
            if (IsJump(move))
            {
                Vector3 delta = playerModel.JumpTime * velocityCurve.Evaluate(z)*velocity;
                playerTransform.DOJump(playerTransform.position + delta + new Vector3(0, 0, 1), playerModel.JumpForce,
                        1, playerModel.JumpTime)
                    .SetEase(Ease.Linear)
                    .OnComplete(OnMoveComplete);
            }
            else
            {
                Vector3 delta = playerModel.MoveTime * velocityCurve.Evaluate(z)*velocity;
                playerTransform.DOMove(playerTransform.position + dir * lineSideDistance + delta, playerModel.MoveTime)
                    .OnComplete(OnMoveComplete);
            }
        }

        private void OnMoveComplete()
        {
            isMoving = false;
            StartSliding();
        }

        public void StartSliding()
        {
            rigidbody.velocity = new Vector3(0,0,velocityCurve.Evaluate(playerTransform.position.z));
        }

        public void StopSliding()
        {
            playerTransform.DOKill();
            rigidbody.velocity = Vector3.zero;
        }


        public bool IsJump(Move move)
        {
            return move == Movement.Move.UP;
        }


        private Vector3 ConsiderMovement(Move move)
        {
            switch (move)
            {
                case Movement.Move.LEFT:
                    if (curLine > 0)
                    {
                        curLine--;
                        return Vector3.left;
                    }

                    break;


                case Movement.Move.RIGHT:
                    if (curLine < lineCount - 1)
                    {
                        curLine++;
                        return Vector3.right;
                    }

                    break;
            }

            return Vector3.zero;
        }

        private Vector3 ConsiderRotation(Move move)
        {
            switch (move)
            {
                case Movement.Move.LEFT:
                    return new Vector3(0, 0, 90);
                case Movement.Move.RIGHT:
                    return new Vector3(0, 0, -90);
                case Movement.Move.BACK:
                    return new Vector3(-90, 0, 0);
            }

            return Vector3.zero;
        }
    }
}
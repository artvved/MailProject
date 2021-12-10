using System.ComponentModel.Design.Serialization;
using DG.Tweening;
using Game.Player;
using UnityEngine;

namespace Game
{
    public class MoveManager
    {
        //player
        private PlayerModel playerModel;
        private Transform playerTransform;
        private float velocity;
        
        //lines
        private float lineSideDistance = 1.2f;
        private int lineCount = 3;
        private int curLine = 1;
        
        //moves
        private float moveTime = 0.2f;
        private bool isMoving = false;
        
        public MoveManager(PlayerModel playerModel,Transform playerTransform)
        {
            this.playerModel = playerModel;
            this.playerTransform = playerTransform;
            velocity = playerModel.Velocity;
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
            var dir = ConsiderMovement(move);
            var rot = ConsiderRotation(move);
            Vector3 delta = moveTime * new Vector3(0,0,velocity);
             
            isMoving = true;
            playerTransform.DORotate(rot, moveTime,RotateMode.WorldAxisAdd).SetEase(Ease.Linear);
            playerTransform.DOMove(playerTransform.position + dir * lineSideDistance+delta, moveTime)
                .OnComplete((() =>
                {
                    isMoving = false;

                }));
        }

      

        private Vector3 ConsiderMovement(Move move)
        {
            switch (move)
            {
                case Game.Move.LEFT:
                    if (curLine>0)
                    {
                        curLine--;
                        return Vector3.left;
                    }
                    else
                    {
                        return Vector3.zero;
                    }


                case Game.Move.RIGHT:
                    if (curLine<lineCount-1)
                    {
                        curLine++;
                        return Vector3.right;
                    }
                    else
                    {
                        return Vector3.zero;
                    }

                case Game.Move.UP:
                    return Vector3.up;
                case Game.Move.BACK:
                    return Vector3.zero;
            }
            return Vector3.zero;
        }
        
        private Vector3 ConsiderRotation(Move move)
        {
            switch (move)
            {
                case Game.Move.LEFT:
                    return new Vector3(0,0,90);
                case Game.Move.RIGHT:
                    return new Vector3(0,0,-90);
                case Game.Move.UP:
                    return Vector3.zero;
                case Game.Move.BACK:
                    return new Vector3(-90,0,0);
            }
            return  Vector3.zero;
        }
        
        
    }
}
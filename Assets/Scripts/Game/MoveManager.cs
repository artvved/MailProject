using System.ComponentModel.Design.Serialization;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace
{
    public class MoveManager
    {

        public MoveManager(Transform transform)
        {
            playerTransform = transform;
        }

        private Transform playerTransform;
        private float lineSideDistance = 1f;
        private float moveTime = 0.5f;
        private bool isMoving = false;
        public void Move(Move move)
        {
            if (!isMoving)
            {

                var dir = ConsiderMovement(move);
                var rot = ConsiderRotation(move);
             
                isMoving = true;
                playerTransform.DORotate(rot, moveTime,RotateMode.WorldAxisAdd).SetEase(Ease.Linear);
                playerTransform.DOMove(playerTransform.position + dir * lineSideDistance, moveTime)
                    .OnComplete((() =>
                    {
                        isMoving = false;

                    }));
            }
        }

        private Vector3 ConsiderMovement(Move move)
        {
            switch (move)
            {
                case DefaultNamespace.Move.LEFT:
                    return Vector3.left;
                case DefaultNamespace.Move.RIGHT:
                    return Vector3.right;
                case DefaultNamespace.Move.UP:
                    return Vector3.up;
                case DefaultNamespace.Move.BACK:
                    return Vector3.zero;
            }
            return Vector3.zero;
        }
        
        private Vector3 ConsiderRotation(Move move)
        {
            switch (move)
            {
                case DefaultNamespace.Move.LEFT:
                    return new Vector3(0,0,90);
                case DefaultNamespace.Move.RIGHT:
                    return new Vector3(0,0,-90);
                case DefaultNamespace.Move.UP:
                    return Vector3.zero;
                case DefaultNamespace.Move.BACK:
                    return new Vector3(-90,0,0);
            }
            return  Vector3.zero;
        }
        
        
    }
}
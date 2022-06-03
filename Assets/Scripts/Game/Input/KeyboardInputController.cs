using System;
using System.Collections;
using System.Collections.Generic;
using Game.Movement;
using UnityEngine;

namespace Game
{
    public class KeyboardInputController : InputController
    {
        public override event Action<Move> InputEvent;
        private KeyCode left = KeyCode.LeftArrow;
        private KeyCode right = KeyCode.RightArrow;
        private KeyCode up = KeyCode.UpArrow;
        private KeyCode back = KeyCode.DownArrow;
        
        
        protected override void MoveAction()
        {
            Move curMove;
            if (Input.GetKeyDown(left))
            {
                curMove = Move.LEFT;
                InputEvent?.Invoke(curMove);
            }else if (Input.GetKeyDown(right))
            {
                curMove = Move.RIGHT;
                InputEvent?.Invoke(curMove);
            }
            else if (Input.GetKeyDown(up))
            {
                curMove = Move.UP;
                InputEvent?.Invoke(curMove);
            }else if (Input.GetKeyDown(back))
            {
                curMove = Move.BACK;
                InputEvent?.Invoke(curMove);
            }

        }
        
        
        void Update()
        {
           MoveAction();
        }
        
       
    }
}
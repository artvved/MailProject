using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace PlayerInput
{
    public class InputController : MonoBehaviour
    {
        public event Action<Move> SwipeEvent;

        private KeyCode left = KeyCode.LeftArrow;
        private KeyCode right = KeyCode.RightArrow;
        private KeyCode up = KeyCode.UpArrow;
        private KeyCode back = KeyCode.DownArrow;
  
    
        void Update()
        {
            Move curMove;
            if (Input.GetKeyDown(left))
            {
                curMove = Move.LEFT;
                SwipeEvent?.Invoke(curMove);
            }else if (Input.GetKeyDown(right))
            {
                curMove = Move.RIGHT;
                SwipeEvent?.Invoke(curMove);
            }
            else if (Input.GetKeyDown(up))
            {
                curMove = Move.UP;
                SwipeEvent?.Invoke(curMove);
            }else if (Input.GetKeyDown(back))
            {
                curMove = Move.BACK;
                SwipeEvent?.Invoke(curMove);
            }
        
        }
    }
}
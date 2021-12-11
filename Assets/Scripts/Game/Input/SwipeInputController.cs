using System;
using UnityEngine;

namespace Game
{
    public class SwipeInputController : MonoBehaviour, IInputable
    {
        public event Action<Move> InputEvent;
        private Vector2 firstPressPos;
        private Vector2 secondPressPos;
        private Vector2 currentSwipe;

        private float minLen = 80;

        void Update()
        {
            Swipe();
        }

        private void Swipe()
        {
            if (Input.GetMouseButtonDown(0))
            {
                firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }

            if (Input.GetMouseButtonUp(0))
            {
                secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

                currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
                if (currentSwipe.magnitude < minLen)
                {
                    return;
                }

                currentSwipe.Normalize();

                if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    InputEvent?.Invoke(Move.UP);
                }
                else if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    InputEvent?.Invoke(Move.BACK);
                }
                else if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    InputEvent?.Invoke(Move.LEFT);
                }
                else if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    InputEvent?.Invoke(Move.RIGHT);
                }
            }
        }
    }
}
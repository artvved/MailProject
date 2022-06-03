using System;
using Game.Movement;
using UnityEngine;

namespace Game
{
    public abstract class InputController : MonoBehaviour
    {
        public abstract event Action<Move> InputEvent;

        protected abstract void MoveAction();
    }
}
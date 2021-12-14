using System;
using Game.Movement;

namespace Game
{
    public interface IInputable 
    {
        public event Action<Move> InputEvent;
    }
}
using System;

namespace Game
{
    public interface IInputable 
    {
        public event Action<Move> InputEvent;
    }
}
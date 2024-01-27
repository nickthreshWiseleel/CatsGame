using System;


namespace Game
{
    public interface ICoroutinePausable : IPausable
    {
        bool IsPaused { get; }
        event Action Paused;
    }
}
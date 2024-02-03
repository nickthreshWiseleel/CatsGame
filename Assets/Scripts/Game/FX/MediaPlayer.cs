using UnityEngine;
using Game.Infrastructure;
using Game.Infrastructure.Pause;

namespace Game
{
    public abstract class MediaPlayer<T> where T : Component
    {
        private readonly PauseManager _pauseManager;

        private readonly T _playable;

        private PrefabFactory<T> _factory;
        private Pool<T> _pool;

        public MediaPlayer(T playable, PauseManager pauseManager)
        {
            _playable = playable;
            _pauseManager = pauseManager;
        }

        public virtual void Init()
        {
            _factory = new(_playable);
            _pool = new(_factory);
        }

        public abstract T Get(T playable);

        public abstract void Return(T playable);
    }
}
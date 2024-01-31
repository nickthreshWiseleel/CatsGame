using UnityEngine;
using Pause;
using Infrastructure;

namespace Game
{
    public abstract class MediaPlayer<T> where T : Component
    {
        private readonly T _playable;
        private readonly PauseManager _pauseManager;
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

        public virtual void PlayVFX(Vector2 position) { }

        public virtual void PlayAudio(AudioClip clip) { }

        public abstract T Get(T playable);

        public abstract void Return(T playable);
    }
}
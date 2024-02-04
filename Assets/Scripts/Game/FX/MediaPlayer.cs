using Game.Infrastructure;
using Game.Infrastructure.Pause;

namespace Game
{
    public abstract class MediaPlayer<T> where T : MediaFX
    {
        protected readonly PauseManager _pauseManager;

        protected readonly T _playable;

        protected PrefabFactory<T> _factory;
        protected Pool<T> _pool;

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
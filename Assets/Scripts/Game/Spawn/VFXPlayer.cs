using UnityEngine;
using Pause;
using Infrastructure;

namespace Game
{
    public class VFXPlayer<T> : MediaPlayer<T> where T : ExplosionVFX, IPausable
    {
        private readonly T _playable;
        private readonly PauseManager _pauseManager;
        private PrefabFactory<T> _factory;
        private Pool<T> _pool;

        public VFXPlayer(T playable, PauseManager pauseManager) : base(playable, pauseManager)
        {
            _playable = playable;
            _pauseManager = pauseManager;
        }

        public override void Init()
        {
            _factory = new(_playable);
            _pool = new(_factory);
        }

        public override void PlayVFX(Vector2 position)
        {
            var playable = Get(_playable);

            playable.OnEffectEnded(playable =>
            {
                Return((T)playable);
            });

            playable.transform.position = position;
            playable.Play();
        }

        public override T Get(T playable)
        {
            playable = _pool.Get(playable);
            _pauseManager.Add(playable);
            return playable;
        }

        public override void Return(T playable)
        {
            _pool.Return(playable);
            _pauseManager.Remove(playable);
        }
    }
}
using UnityEngine;
using Game.Infrastructure.Pause;

namespace Game
{
    public class VFXPlayer<T> : MediaPlayer<T> where T : VFX
    {
        public VFXPlayer(T playable, PauseManager pauseManager) : base(playable, pauseManager)
        {
        }

        public void Play(Vector2 position)
        {
            var playable = Get(_playable);

            playable.OnEffectEnded(_ => Return(playable));

            playable.transform.position = position;
            playable.Play();
        }

        public override T Get(T playable)
        {
            playable = _pool.Get();
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
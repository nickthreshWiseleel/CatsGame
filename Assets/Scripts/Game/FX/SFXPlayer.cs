using UnityEngine;
using Game.Infrastructure.Pause;

namespace Game
{
    public class SFXPlayer<T> : MediaPlayer<T> where T : SFX
    {
        public SFXPlayer(T playable, PauseManager pauseManager) : base(playable, pauseManager)
        {
        }

        public void Play(AudioClip clip)
        {
            var playable = Get(_playable);

            playable.OnEffectEnded(_ => Return(playable));

            playable.SetPlayEffect(clip);
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
using System;
using UnityEngine;
using Game.Infrastructure.Pause;

namespace Game
{
    public abstract class MediaFX : MonoBehaviour, IPausable
    {
        protected Action<MediaFX> _ended;

        public virtual MediaFX OnEffectEnded(Action<MediaFX> ended)
        {
            _ended = ended;
            return this;
        }

        public abstract void Play();

        public abstract void Pause();

        public abstract void Unpause();
    }
}
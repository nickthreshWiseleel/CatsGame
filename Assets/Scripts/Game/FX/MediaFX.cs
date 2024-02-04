using System;
using UnityEngine;
using Game.Infrastructure.Pause;

namespace Game
{
    public abstract class MediaFX : MonoBehaviour, IPausable
    {
        public abstract MediaFX OnEffectEnded(Action<MediaFX> ended);

        public abstract void Play();

        public abstract void Pause();

        public abstract void Unpause();
    }
}
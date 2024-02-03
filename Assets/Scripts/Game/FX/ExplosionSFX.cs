using System;
using System.Collections;
using UnityEngine;
using Game.Infrastructure.Pause;

namespace Game
{
    [RequireComponent(typeof(AudioSource))]
    public class ExplosionSFX : MonoBehaviour, IMedia<ExplosionSFX>, ICustomSFX<AudioClip>, IPausable
    {
        private AudioSource _audioSource;

        private Action<ExplosionSFX> _ended;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void SetPlayEffect(AudioClip clip)
        {
            _audioSource.clip = clip;
        }

        public void Play()
        {
            StartCoroutine(PlaySound());
        }

        public ExplosionSFX OnEffectEnded(Action<ExplosionSFX> ended)
        {
            _ended = ended;
            return this;
        }

        private IEnumerator PlaySound()
        {
            _audioSource.Play();

            yield return new WaitForSeconds(_audioSource.clip.length);

            _ended?.Invoke(this);
        }

        public void Pause()
        {
            _audioSource.Pause();
        }

        public void Unpause()
        {
            _audioSource.UnPause();
        }
    }
}
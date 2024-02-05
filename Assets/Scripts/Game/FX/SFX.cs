using System.Collections;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(AudioSource))]
    public class SFX : MediaFX
    {
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void SetPlayEffect(AudioClip clip)
        {
            _audioSource.clip = clip;
        }

        public override void Play()
        {
            StartCoroutine(PlaySound());
        }

        private IEnumerator PlaySound()
        {
            _audioSource.Play();

            yield return new WaitForSeconds(_audioSource.clip.length);

            _ended?.Invoke(this);
        }

        public override void Pause()
        {
            _audioSource.Pause();
        }

        public override void Unpause()
        {
            _audioSource.UnPause();
        }
    }
}
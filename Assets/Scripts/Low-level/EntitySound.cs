using System;
using System.Collections;
using UnityEngine;


namespace Game
{
    [RequireComponent(typeof(AudioSource))]
    public class EntitySound : MonoBehaviour, IPausable
    {
        private AudioSource _audioSource;
        private Action<EntitySound> _ended;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void Init()
        {
            StartCoroutine(Play());
        }

        public EntitySound OnAudioEnded(Action<EntitySound> ended)
        {
            _ended = ended;
            return this;
        }

        private IEnumerator Play()
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
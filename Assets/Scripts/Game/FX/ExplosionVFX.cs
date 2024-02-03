using System;
using UnityEngine;
using Game.Infrastructure.Pause;

namespace Game
{
    [RequireComponent(typeof(Animator))]
    public class ExplosionVFX : MonoBehaviour, IMedia<ExplosionVFX>, IPausable
    {
        private readonly int _isDestroyed = Animator.StringToHash("isDestroyed");
        private Animator _animator;

        private Action<ExplosionVFX> _ended;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Play()
        {
            _animator.SetBool(_isDestroyed, true);
        }

        private void EffectEnded() //animation event
        {
            _ended?.Invoke(this);
        }

        public ExplosionVFX OnEffectEnded(Action<ExplosionVFX> ended)
        {
            _ended = ended;
            return this;
        }

        public void Pause()
        {
            _animator.speed = 0f;
        }

        public void Unpause()
        {
            _animator.speed = 1f;
        }
    }
}
using System;
using UnityEngine;


namespace Game
{
    [RequireComponent(typeof(Animator))]
    public class ExplosionVFX : MonoBehaviour, IPausable
    {
        private Animator _animator;
        private readonly int _isDestroyed = Animator.StringToHash("isDestroyed");
        private Action<ExplosionVFX> _ended;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Play()
        {
            _animator.SetBool(_isDestroyed, true);
        }

        public void AnimationEnded()
        {
            _ended?.Invoke(this);
        }

        public ExplosionVFX OnAnimationEnded(Action<ExplosionVFX> ended)
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
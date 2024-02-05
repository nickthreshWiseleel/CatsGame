using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Animator))]
    public class VFX : MediaFX
    {
        private readonly int _isDestroyed = Animator.StringToHash("isDestroyed");
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public override void Play()
        {
            _animator.SetBool(_isDestroyed, true);
        }

        private void EffectEnded() //animation event
        {
            _ended?.Invoke(this);
        }

        public override void Pause()
        {
            _animator.speed = 0f;
        }

        public override void Unpause()
        {
            _animator.speed = 1f;
        }
    }
}
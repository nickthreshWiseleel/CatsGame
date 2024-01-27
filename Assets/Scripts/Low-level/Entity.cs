using System;
using System.Collections;
using UnityEngine;


namespace Game
{
    public class Entity : MonoBehaviour, IHitable, ICoroutinePausable
    {
        private Coroutine _delay;
        private Action<Entity> _destroyed;
        private Action<Entity> _hitted;
        private bool _isPaused;

        public event Action Paused;
        public bool IsPaused => _isPaused;

        public void Init(float lifeDelay)
        {
            _delay = StartCoroutine(Delay(lifeDelay));
        }

        public Entity OnDestroyed(Action<Entity> action)
        {
            _destroyed = action;
            return this;
        }

        public Entity OnHitted(Action<Entity> action)
        {
            _hitted = action;
            return this;
        }

        public void Hit()
        {
            StopCoroutine(_delay);

            _hitted?.Invoke(this);
        }

        private IEnumerator Delay(float lifeDelay)
        {
            yield return new PausableWaitForSeconds(lifeDelay, this);

            _destroyed?.Invoke(this);
        }

        public void Pause()
        {
            _isPaused = true;

            Paused?.Invoke();
        }

        public void Unpause()
        {
            _isPaused = false;
        }
    }
}
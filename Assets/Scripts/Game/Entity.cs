using System;
using System.Collections;
using UnityEngine;
using Game.Infrastructure.Pause;

namespace Game
{
    public class Entity : MonoBehaviour, IHitable, IPausable
    {
        private Coroutine _delay;

        private PauseToken _pauseToken;

        private Action<Entity> _destroyed;
        private Action<Entity> _hitted;

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

        public void Pause()
        {
            _pauseToken?.Pause();
        }

        public void Unpause()
        {
            _pauseToken?.Unpause();
        }

        private IEnumerator Delay(float lifeDelay)
        {
            _pauseToken = new();

            yield return new PausableWaitForSeconds(lifeDelay, _pauseToken);

            _destroyed?.Invoke(this);
        }
    }
}
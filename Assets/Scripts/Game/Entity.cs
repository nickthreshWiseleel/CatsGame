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

        private GameConfig _config;

        private Action<Entity, Rewards> _destroyed;
        private Action<Entity, Rewards> _hitted;

        public void Init(float lifeDelay, GameConfig config)
        {
            _delay = StartCoroutine(Delay(lifeDelay));
            _config = config;
        }

        public Entity OnDestroyed(Action<Entity, Rewards> action)
        {
            _destroyed = action;
            return this;
        }

        public Entity OnHitted(Action<Entity, Rewards> action)
        {
            _hitted = action;
            return this;
        }

        public void Hit()
        {
            StopCoroutine(_delay);

            Rewards rewards = new(
                health: 0,
                score: _config.ScoreIncrement,
                money: _config.MoneyIncrement,
                destroyed: _config.DestroyedIncrement);

            _hitted?.Invoke(this, rewards);
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

            Rewards rewards = new(
                health: _config.HealthDecrement,
                score: 0,
                money: 0,
                destroyed: 0);

            _destroyed?.Invoke(this, rewards);
        }
    }
}
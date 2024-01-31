using System;

namespace Game
{
    public class GameRules
    {
        private readonly Session _session;
        private readonly GameConfig _config;

        public event Action Attacked;
        public event Action Destroyed;

        public GameRules(Session session, GameConfig gameConfig)
        {
            _session = session;
            _config = gameConfig;
        }

        public void Init()
        {
            _session.Subscribe();
            Attacked?.Invoke();
            Destroyed?.Invoke();
        }

        public void CatAttacks()
        {
            _session.Health.Value += _config.HealthDecrement;

            Attacked?.Invoke();
        }

        public void CatIsDestroyed()
        {
            _session.Score.Value += _config.ScoreIncrement;
            _session.Money.Value += _config.MoneyIncrement;
            _session.Destroyed.Value += _config.DestroyedIncrement;

            Destroyed?.Invoke();
        }
    }
}
namespace Game
{
    public class GameRules
    {
        private readonly Session _session;

        private readonly GameConfig _config;

        public GameRules(Session session, GameConfig gameConfig)
        {
            _session = session;
            _config = gameConfig;
        }

        public void CatAttacks()
        {
            _session.Health.Value += _config.HealthDecrement;
        }

        public void CatIsDestroyed()
        {
            _session.Score.Value += _config.ScoreIncrement;
            _session.Money.Value += _config.MoneyIncrement;
            _session.Destroyed.Value += _config.DestroyedIncrement;
        }
    }
}
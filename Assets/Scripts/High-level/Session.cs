using System;


namespace Game
{
    public class Session
    {
        private int _health, _score, _money, _destroyed;
        private readonly GameConfig _dataConfig;

        public bool IsDebugging { get; set; }
        public int Health => _health;
        public int Score => _score;
        public int Money => _money;
        public int Destroyed => _destroyed;

        public event Action Changed;

        public Session(GameConfig config)
        {
            _dataConfig = config;
            _health = config.StartHealth;
            _score = config.StartScore;
            _money = config.StartMoney;
            _destroyed = config.StartDestroyed;
        }

        public void CatAttacked()
        {
            if (IsDebugging == true)
            {
                return;
            }

            _health += _dataConfig.HealthDecrement;

            Changed?.Invoke();
        }

        public void CatWasDestroyed()
        {
            _score += _dataConfig.ScoreIncrement;
            _money += _dataConfig.MoneyIncrement;
            _destroyed += _dataConfig.DestroyedIncrement;

            Changed?.Invoke();
        }
    }
}
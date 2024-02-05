using UniRx;

namespace Game
{
    public class Session
    {
        private readonly ReactiveProperty<int> _health;
        private readonly ReactiveProperty<int> _score;
        private readonly ReactiveProperty<int> _money;
        private readonly ReactiveProperty<int> _destroyed;

        public ReactiveProperty<int> Health => _health;
        public ReactiveProperty<int> Score => _score;
        public ReactiveProperty<int> Money => _money;
        public ReactiveProperty<int> Destroyed => _destroyed;

        public Session(GameConfig config)
        {
            _health = new(config.StartHealth);
            _score = new(config.StartScore);
            _money = new(config.StartMoney);
            _destroyed = new(config.StartDestroyed);
        }
    }
}
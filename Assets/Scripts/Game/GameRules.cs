using Game.Infrastructure.Pause;
using UniRx;

namespace Game
{
    public class GameRules
    {
        private readonly Session _session;

        private readonly CompositeDisposable _disposable = new();

        public GameRules(Session session, IPauseProvider pauseProvider, LoseScreen loadScreen)
        {
            _session = session;

            _session.Health.Where(x => x <= 0).Subscribe(_ =>
            {
                loadScreen.Show();
                pauseProvider.Pause();
                _disposable.Dispose();
            }).AddTo(_disposable);
        }

        public void ApplyRewards(Rewards rewards)
        {
            _session.Health.Value += rewards.Health;
            _session.Score.Value += rewards.Score;
            _session.Money.Value += rewards.Money;
            _session.Destroyed.Value += rewards.Destroyed;
        }
    }
}
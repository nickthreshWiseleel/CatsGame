using System;
using UniRx;

namespace Game
{
    public class Session
    {
        public readonly ReactiveProperty<int> Health = new();
        public readonly ReactiveProperty<int> Score = new();
        public readonly ReactiveProperty<int> Money = new();
        public readonly ReactiveProperty<int> Destroyed = new();

        private readonly CompositeDisposable _disposable = new();

        public event Action Changed;

        public Session(GameConfig config)
        {
            Health.Value = config.StartHealth;
            Score.Value = config.StartScore;
            Money.Value = config.StartMoney;
            Destroyed.Value = config.StartDestroyed;
        }

        public void Subscribe()
        {
            Observable.Merge(Health, Score, Money, Destroyed).Subscribe(_ =>
            {
                Changed?.Invoke();
            }).AddTo(_disposable);
        }
    }
}
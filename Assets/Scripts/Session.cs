using System;

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

        ChangeData(ref _health, _dataConfig.HealthDecrement);

        Changed?.Invoke();
    }

    public void CatWasDestroyed()
    {
        ChangeData(ref _score, _dataConfig.ScoreIncrement);
        ChangeData(ref _money, _dataConfig.MoneyIncrement);
        ChangeData(ref _destroyed, _dataConfig.DestroyedIncrement);

        Changed?.Invoke();
    }

    private void ChangeData(ref int parameter, int parameterValue)
    {
        parameter += parameterValue;
    }
}

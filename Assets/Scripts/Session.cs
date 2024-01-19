using System.Threading;

public class Session
{
    private int _health, _score, _money, _destroyed;
    private DataConfig _dataConfig;

    public bool IsDebugging { get; set; }
    public int Health => _health;
    public int Score => _score;
    public int Money => _money;
    public int Destroyed => _destroyed;

    public Session(DataConfig config, Lifetime lifeTime)
    {
        _dataConfig = config;
        _health = config.StartHealth;
        _score = config.StartScore;
        _money = config.StartMoney;
        _destroyed = config.StartDestroyed;

        lifeTime.CatWasDestroyed += OnCatWasDestroyed;
        lifeTime.CatAttacked += OnCatAttacked;
    }

    private void OnCatAttacked()
    {
        if (IsDebugging == true)
        {
            return;
        }

        ChangeData(ref _health, _dataConfig.HealthDecrement);
    }

    private void OnCatWasDestroyed()
    {
        ChangeData(ref _score, _dataConfig.ScoreIncrement);
        ChangeData(ref _money, _dataConfig.MoneyIncrement);
        ChangeData(ref _destroyed, _dataConfig.DestroyedIncrement);
    }

    public void ChangeData(ref int parameter, int parameterValue)
    {
        parameter += parameterValue;
    }
}

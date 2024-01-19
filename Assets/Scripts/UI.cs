using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private Lifetime _lifetime;
    [SerializeField] private TextMeshProUGUI _healthValue;
    [SerializeField] private TextMeshProUGUI _scoreValue;
    [SerializeField] private TextMeshProUGUI _moneyValue;
    [SerializeField] private TextMeshProUGUI _destroyedValue;
    [SerializeField] private TextMeshProUGUI _poolCount;
    public void ShowData()
    {
        _healthValue.text = _lifetime.Session.Health.ToString();
        _scoreValue.text = _lifetime.Session.Score.ToString();
        _moneyValue.text = _lifetime.Session.Money.ToString();
        _destroyedValue.text = _lifetime.Session.Destroyed.ToString();
        //_poolCount.text = _lifetime.PoolCount.ToString();
    }

    private void OnEnable()
    {
        _lifetime.CatWasDestroyed += ShowData;
        _lifetime.CatAttacked += ShowData;
        _lifetime.Updated += ShowData;
    }

    private void OnDisable()
    {
        _lifetime.CatWasDestroyed -= ShowData;
        _lifetime.CatAttacked -= ShowData;
        _lifetime.Updated -= ShowData;
    }
}

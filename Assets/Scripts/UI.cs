using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _healthValue;
    [SerializeField] private TextMeshProUGUI _scoreValue;
    [SerializeField] private TextMeshProUGUI _moneyValue;
    [SerializeField] private TextMeshProUGUI _destroyedValue;
    private Session _session;

    
    public void ShowData()
    {
        _healthValue.text = _session.Health.ToString();
        _scoreValue.text = _session.Score.ToString();
        _moneyValue.text = _session.Money.ToString();
        _destroyedValue.text = _session.Destroyed.ToString();
    }

    public void Init(Session session)
    {
        _session = session;
    }

    private void OnEnable()
    {
        _session.Changed += ShowData;
    }

    private void OnDisable()
    {
        _session.Changed -= ShowData;
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Game.Infrastructure.Pause;

namespace Game
{
    public class UI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _healthValue;
        [SerializeField] private TextMeshProUGUI _scoreValue;
        [SerializeField] private TextMeshProUGUI _moneyValue;
        [SerializeField] private TextMeshProUGUI _destroyedValue;

        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _unpauseButton;

        [SerializeField] private Image _pauseMenu;

        private IPauseProvider _pauseProvider;

        private Session _session;

        private CompositeDisposable _disposable = new();

        public void Init(Session session, IPauseProvider pauseProvider)
        {
            _session = session;
            _pauseProvider = pauseProvider;

            ShowData();

            _session.Health.Subscribe(x => _healthValue.text = x.ToString()).AddTo(_disposable);
            _session.Score.Subscribe(x => _scoreValue.text = x.ToString()).AddTo(_disposable);
            _session.Money.Subscribe(x => _moneyValue.text = x.ToString()).AddTo(_disposable);
            _session.Destroyed.Subscribe(x => _destroyedValue.text = x.ToString()).AddTo(_disposable);
        }

        public void ShowData()
        {
            _healthValue.text = _session.Health.Value.ToString();
            _scoreValue.text = _session.Score.Value.ToString();
            _moneyValue.text = _session.Money.Value.ToString();
            _destroyedValue.text = _session.Destroyed.Value.ToString();
        }

        public void PauseButton()
        {
            _pauseProvider.Pause();
            _pauseMenu.gameObject.SetActive(true);
        }

        public void UnpauseButton()
        {
            _pauseProvider.Unpause();
            _pauseMenu.gameObject.SetActive(false);
        }
    }
}
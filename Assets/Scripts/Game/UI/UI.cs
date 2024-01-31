using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Pause;

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
        private GameRules _gameRules;

        public void Init(Session session, GameRules gameRules, IPauseProvider pauseProvider)
        {
            _session = session;
            _gameRules = gameRules;
            _pauseProvider = pauseProvider;
            _gameRules.Attacked += ShowData;
            _gameRules.Destroyed += ShowData;
            ShowData();
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
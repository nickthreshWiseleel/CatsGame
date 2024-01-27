using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Game
{
    public class UI : MonoBehaviour
    {
        [SerializeField] private Lifetime _lifetime;
        [SerializeField] private TextMeshProUGUI _healthValue;
        [SerializeField] private TextMeshProUGUI _scoreValue;
        [SerializeField] private TextMeshProUGUI _moneyValue;
        [SerializeField] private TextMeshProUGUI _destroyedValue;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _unpauseButton;
        [SerializeField] private Image _pauseMenu;
        private IPauseProvider _pauseProvider;
        private Session _session;

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

        public void ShowData()
        {
            _healthValue.text = _session.Health.ToString();
            _scoreValue.text = _session.Score.ToString();
            _moneyValue.text = _session.Money.ToString();
            _destroyedValue.text = _session.Destroyed.ToString();
        }

        public void Init(Session session, Lifetime lifetime, IPauseProvider pauseProvider)
        {
            _session = session;
            _lifetime = lifetime;
            _pauseProvider = pauseProvider;
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
}
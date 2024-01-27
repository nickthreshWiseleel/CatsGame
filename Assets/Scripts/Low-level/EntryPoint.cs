using UnityEngine;


namespace Game
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private Lifetime _lifetime;
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private Entity _entity;
        [SerializeField] private ExplosionVFX _explosion;
        [SerializeField] private EntitySound _explosionEntitySound;
        [SerializeField] private EntitySound _clickEntitySound;
        [SerializeField] private RectTransform _spawnArea;
        [SerializeField] private float _spawnDelay;
        [SerializeField] private float _lifeDelay;
        [SerializeField] private UI _UI;

        private Spawner _spawner;
        private Session _session;
        private VFXPlayer _VFXPlayer;
        private SoundPlayer _explosionSoundPlayer;
        private SoundPlayer _clickSoundPlayer;
        private PauseManager _pauseManager = new();

        private void Awake()
        {

            _spawner = new(_entity, _spawnArea, _lifeDelay);
            _spawner.Init();

            _session = new(_gameConfig);

            _VFXPlayer = new(_explosion, _pauseManager);
            _VFXPlayer.Init();

            _explosionSoundPlayer = new(_explosionEntitySound, _pauseManager);
            _explosionSoundPlayer.Init();

            _clickSoundPlayer = new(_clickEntitySound, _pauseManager);
            _clickSoundPlayer.Init();

            _lifetime.Init(_entity, _spawner, _session, _pauseManager, _VFXPlayer, _explosionSoundPlayer, _clickSoundPlayer, _spawnDelay);

            _UI.Init(_session, _lifetime, _pauseManager);
        }
    }
}
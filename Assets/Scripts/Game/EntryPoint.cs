using UnityEngine;
using Game.Infrastructure.Pause;

namespace Game
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private Lifetime _lifetime;

        [SerializeField] private Entity _entity;

        [SerializeField] private GameConfig _gameConfig;
        
        [SerializeField] private AudioConfig _explosionSounds;
        [SerializeField] private AudioConfig _clickSounds;
        
        [SerializeField] private VFX _animationPrefab;
        [SerializeField] private SFX _soundPrefab;

        [SerializeField] private RectTransform _spawnArea;

        [SerializeField] private LoseScreen _loseScreen;

        [SerializeField] private float _spawnDelay;
        [SerializeField] private float _lifeDelay;
        
        [SerializeField] private UI _UI;

        private void Awake()
        {
            PauseManager pauseManager = new();

            Spawner spawner = new(_entity, _spawnArea, _gameConfig, _lifeDelay);
            spawner.Init();

            Session session = new(_gameConfig);

            GameRules gameRules = new(session, pauseManager, _loseScreen);

            VFXPlayer<VFX> VFXPlayer = new(_animationPrefab, pauseManager);
            VFXPlayer.Init();

            SFXPlayer<SFX> audioPlayer = new(_soundPrefab, pauseManager);
            audioPlayer.Init();

            _lifetime.Init(spawner, gameRules, pauseManager, VFXPlayer, audioPlayer, _clickSounds, _explosionSounds, _spawnDelay);

            _UI.Init(session, pauseManager);
        }
    }
}
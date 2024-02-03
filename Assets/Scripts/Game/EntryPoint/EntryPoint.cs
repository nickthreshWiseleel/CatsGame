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
        
        [SerializeField] private ExplosionVFX _animationPrefab;
        [SerializeField] private ExplosionSFX _soundPrefab;

        [SerializeField] private RectTransform _spawnArea;

        [SerializeField] private float _spawnDelay;
        [SerializeField] private float _lifeDelay;
        
        [SerializeField] private UI _UI;

        private void Awake()
        {
            PauseManager pauseManager = new();

            Spawner spawner = new(_entity, _spawnArea, _lifeDelay);
            spawner.Init();

            Session session = new(_gameConfig);

            GameRules gameRules = new(session, _gameConfig);

            VFXPlayer<ExplosionVFX> VFXPlayer = new(_animationPrefab, pauseManager);
            VFXPlayer.Init();

            SFXPlayer<ExplosionSFX> audioPlayer = new(_soundPrefab, pauseManager);
            audioPlayer.Init();

            _lifetime.Init(_entity, spawner, gameRules, pauseManager, VFXPlayer, audioPlayer, _clickSounds, _explosionSounds, _spawnDelay);

            _UI.Init(session, pauseManager);
        }
    }
}
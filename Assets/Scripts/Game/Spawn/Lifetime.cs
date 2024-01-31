using System.Diagnostics;
using UnityEngine;
using Pause;

namespace Game
{
    public class Lifetime : MonoBehaviour, IPausable
    {
        [Range(0f, 1f), SerializeField] private float _speed = 1f;
        private Entity _entity;
        private Spawner _spawner;
        private PauseManager _pauseManager;
        private VFXPlayer<ExplosionVFX> _VFXPlayer;
        private SoundPlayer<EntitySound> _soundPlayer;
        private AudioConfig _explosionSounds;
        private AudioConfig _clickSounds;
        private float _spawnDelay;

        private float _time;
        private bool _isPaused;

        private GameRules _gameRules;

        public void Init(Entity entity,
            Spawner spawner,
            GameRules rules,
            PauseManager pauseManager,
            VFXPlayer<ExplosionVFX> VFXPlayer,
            SoundPlayer<EntitySound> soundPlayer,
            AudioConfig clickSounds,
            AudioConfig explosionSounds,
            float spawnDelay)
        {
            _entity = entity;
            _spawner = spawner;
            _gameRules = rules;
            _VFXPlayer = VFXPlayer;
            _soundPlayer = soundPlayer;
            _clickSounds = clickSounds;
            _explosionSounds = explosionSounds;
            _spawnDelay = spawnDelay;
            _pauseManager = pauseManager;
            _pauseManager.Add(this);
        }

        private void Update()
        {
            Time.timeScale = _speed;

            if (_isPaused) return;

            _time += Time.deltaTime;

            if (_time >= _spawnDelay)
            {
                Spawn(_entity);

                _time = 0;
            }
        }

        public void Pause()
        {
            _isPaused = true;
        }

        public void Unpause()
        {
            _isPaused = false;
        }

        private void Spawn(Entity entity)
        {
            entity = _spawner.Spawn();

            _pauseManager.Add(entity);


            entity
                .OnHitted(entity =>
                {
                    _gameRules.CatIsDestroyed();

                    _spawner.Return(entity);

                    _VFXPlayer.PlayVFX(entity.transform.position);

                    _soundPlayer.PlayAudio(_clickSounds.audioList[0]);

                    _pauseManager.Remove(entity);
                })
                .OnDestroyed(entity =>
                {
                    _gameRules.CatAttacks();

                    _spawner.Return(entity);

                    _VFXPlayer.PlayVFX(entity.transform.position);

                    var audio = _explosionSounds.audioList[Random.Range(0, _explosionSounds.audioList.Count)];
                    _soundPlayer.PlayAudio(audio);

                    _pauseManager.Remove(entity);
                });
        }
    }
}
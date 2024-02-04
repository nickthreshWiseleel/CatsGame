﻿using UnityEngine;
using Game.Infrastructure.Pause;

namespace Game
{
    public class Lifetime : MonoBehaviour, IPausable
    {
        private Entity _entity;
        private Spawner _spawner;

        private GameRules _gameRules;

        private PauseManager _pauseManager;

        private VFXPlayer<VFX> _VFXPlayer;
        private SFXPlayer<SFX> _SFXPlayer;

        private AudioConfig _explosionSounds;
        private AudioConfig _clickSounds;

        private float _spawnDelay;

        private float _time;

        private bool _isPaused;

        public void Init(
            Entity entity,
            Spawner spawner,
            GameRules rules,
            PauseManager pauseManager,
            VFXPlayer<VFX> VFXPlayer,
            SFXPlayer<SFX> SFXPlayer,
            AudioConfig clickSounds,
            AudioConfig explosionSounds,
            float spawnDelay)
        {
            _entity = entity;
            _spawner = spawner;
            _gameRules = rules;
            _pauseManager = pauseManager;
            _VFXPlayer = VFXPlayer;
            _SFXPlayer = SFXPlayer;
            _clickSounds = clickSounds;
            _explosionSounds = explosionSounds;
            _spawnDelay = spawnDelay;

            _pauseManager.Add(this);
        }

        private void Update()
        {
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

                    Despawn(entity, _clickSounds.audioList[0]);
                })
                .OnDestroyed(entity =>
                {
                    _gameRules.CatAttacks();

                    var audio = _explosionSounds.audioList[Random.Range(0, _explosionSounds.audioList.Count)];

                    Despawn(entity, audio);
                });
        }

        private void Despawn(Entity entity, AudioClip soundClip)
        {
            _spawner.Return(entity);

            _VFXPlayer.Play(entity.transform.position);

            _SFXPlayer.Play(soundClip);

            _pauseManager.Remove(entity);
        }
    }
}
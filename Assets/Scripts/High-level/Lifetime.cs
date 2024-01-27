using UnityEngine;


namespace Game
{
    public class Lifetime : MonoBehaviour, IPausable
    {
        private Entity _entity;
        private Spawner _spawner;
        private Session _session;
        private PauseManager _pauseManager;
        private VFXPlayer _VFXPlayer;
        private SoundPlayer _explosionSoundPlayer;
        private SoundPlayer _clickSoundPlayer;
        private float _spawnDelay;

        private float _time;
        private bool _isPaused;

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

        private void Spawn(Entity entity)
        {
            entity = _spawner.Spawn();

            _pauseManager.Add(entity);

            entity
                .OnHitted(entity =>
                {
                    _session.CatWasDestroyed();
                    _spawner.Return(entity);
                    _VFXPlayer.Play(entity.transform.position);
                    _clickSoundPlayer.Play(entity.transform.position);
                    _pauseManager.Remove(entity);
                })
                .OnDestroyed(entity =>
                {
                    _session.CatAttacked();
                    _spawner.Return(entity);
                    _VFXPlayer.Play(entity.transform.position);
                    _explosionSoundPlayer.Play(entity.transform.position);
                    _pauseManager.Remove(entity);
                });
        }

        public void Init(Entity entity, Spawner spawner, Session session, PauseManager pauseManager, VFXPlayer VFXPlayer, SoundPlayer explosionSoundPlayer, SoundPlayer clickSoundPlayer, float spawnDelay)
        {
            _entity = entity;
            _spawner = spawner;
            _session = session;
            _VFXPlayer = VFXPlayer;
            _explosionSoundPlayer = explosionSoundPlayer;
            _clickSoundPlayer = clickSoundPlayer;
            _spawnDelay = spawnDelay;
            _pauseManager = pauseManager;
            _pauseManager.Add(this);
        }

        public void Pause()
        {
            _isPaused = true;
        }

        public void Unpause()
        {
            _isPaused = false;
        }
    }
}
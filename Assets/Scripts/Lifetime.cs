using System;
using UnityEngine;

public class Lifetime : MonoBehaviour, IPausable
{
    [Range(0f, 1f), SerializeField] private float _speed = 1f;
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private Entity _entity;
    [SerializeField] private Explosion _explosion;
    [SerializeField] private RectTransform _spawnerArea;
    [SerializeField] private UI _UI;
    [SerializeField] private float _timeSinceSpawn;
    [SerializeField] private float _scale;
    [SerializeField] private float _lifeDelay;
    private float _time;
    private Session _session;
    private bool _isPaused;

    private Spawner _entitySpawner;

    private VFXPlayer _VFXPlayer;
    private SoundPlayer _soundPlayer;

    public Session Session => _session;

    private void Awake()
    {
        _entitySpawner = new(_entity, _spawnerArea, _lifeDelay);

        _session = new(_gameConfig);

        _VFXPlayer = new(_explosion);

        _UI.Init(_session);
    }

    private void Update()
    {
        if (_isPaused) return;

        _time += Time.deltaTime;
        Time.timeScale = _speed;

        if (_time >= _timeSinceSpawn)
        {
            var entity = _entitySpawner.Spawn();

            entity
                .OnHitted(entity =>
                {
                    _session.CatAttacked();
                    _entitySpawner.Return(entity);
                    _VFXPlayer.Play(entity.transform.position);
                })
                .OnDestroyed(entity =>
                {
                    _session.CatWasDestroyed();
                    _entitySpawner.Return(entity);
                    _VFXPlayer.Play(entity.transform.position);
                });

            _time = 0;
        }
    }

    public void Init(VFXPlayer VFXPlayer, SoundPlayer soundPlayer)
    {
        _VFXPlayer = VFXPlayer;
        _soundPlayer = soundPlayer;
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

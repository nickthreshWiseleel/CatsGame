using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifetime : MonoBehaviour, IPausable
{
    [Range(0f, 1f), SerializeField] private float _speed = 1f;
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private Entity _entity;
    [SerializeField] private Explosion _explosion;
    [SerializeField] private RectTransform _spawnArea;
    [SerializeField] private float _timeSinceSpawn;
    [SerializeField] private float _scale;
    [SerializeField] private float _lifeDelay;
    private float _time;
    private Session _session;
    private Factory<Entity> _entityFactory;
    private Pool<Entity> _entityPool;
    private bool _isPaused;

    private Factory<Explosion> _explosionFactory;
    private Pool<Explosion> _explosionPool;

    private VFXPlayer _VFXPlayer;
    private SoundPlayer _soundPlayer;

    private float _highestCornerX;
    private float _highestCornerY;
    private float _lowestCornerX;
    private float _lowestCornerY;

    public Session Session => _session;

    public event Action CatWasDestroyed;
    public event Action CatAttacked;
    public event Action Updated;

    private void Awake()
    {
        _entityFactory = new(_entity, _scale, _lifeDelay);
        _entityPool = new(_entityFactory);
        _session = new(_gameConfig);

        _explosionFactory = new(_explosion, _scale, _explosion.Sprite);
        _explosionPool = new(_explosionFactory);
    }

    private void Update()
    {
        if (_isPaused) return;

        _time += Time.deltaTime;
        Time.timeScale = _speed;

        if (_time >= _timeSinceSpawn)
        {
            var entity = _entityPool.Get(_entity);
            SetPosition(entity, _spawnArea);

            entity
                .OnHitted(entity =>
                {
                    _session.CatAttacked();
                    _entityPool.Return(entity);
                })
                .OnDestroyed(entity =>
                {
                    _session.CatWasDestroyed();
                    _entityPool.Return(entity);
                });

            _time = 0;
        }
    }

    public void Init(VFXPlayer VFXPlayer, SoundPlayer soundPlayer)
    {
        _VFXPlayer = VFXPlayer;
        _soundPlayer = soundPlayer;
    }

    private void SetPosition(Entity entity, RectTransform spawnArea)
    {
        var menuCorners = new Vector3[4];
        spawnArea.GetWorldCorners(menuCorners);
        foreach (var corner in menuCorners)
        {
            if (corner.x > 0 && corner.y > 0)
            {
                _highestCornerX = corner.x;
                _highestCornerY = corner.y;
            }

            if (corner.x < 0 && corner.y < 0)
            {
                _lowestCornerX = corner.x;
                _lowestCornerY = corner.y;
            }
        }

        var horizontal = UnityEngine.Random.Range(_lowestCornerX, _highestCornerX);
        var vertical = UnityEngine.Random.Range(_lowestCornerY, _highestCornerY);

        entity.gameObject.transform.position = new Vector2(horizontal, vertical);
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

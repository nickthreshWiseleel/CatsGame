using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UIElements;

public class Lifetime : MonoBehaviour, IPausable
{
    [Range(0f, 1f), SerializeField] private float _speed = 1f;
    [SerializeField] private DataConfig _dataConfig;
    [SerializeField] private Entity _cat;
    [SerializeField] private EntryPoint _entryPoint;
    [SerializeField] private EntityConfig _entityConfig;
    [SerializeField] private AnimationConfig _animationConfig;
    [SerializeField] private RectTransform _spawnArea;
    [SerializeField] private float _timeSinceSpawn;
    [SerializeField] private float _scale;
    [SerializeField] private float _lifeDelay;
    [SerializeField] private float _animationDelay;
    private float _time;
    private Session _session;
    private EntityFactory _factory;
    private Pool<Entity> _pool;
    private bool _isPaused;

    private List<Coroutine> _coroutines = new();

    private float _highestCornerX;
    private float _highestCornerY;
    private float _lowestCornerX;
    private float _lowestCornerY;

    public Session Session => _session;
    public int PoolCount => _pool.Count;

    public event Action CatWasDestroyed;
    public event Action CatAttacked;
    public event Action Updated;

    private void Awake()
    {
        _cat.InitConfig(_entityConfig);
        _factory = new(_cat, _entityConfig);
        _pool = new(_factory);
        _session = new(_dataConfig, this);
    }

    private void Update()
    {
        if (_isPaused) return;

        _time += Time.deltaTime;
        Time.timeScale = _speed;

        if (_time >= _timeSinceSpawn)
        {
            var obj = _pool.Get(_cat);
            SetScale(obj, _scale);
            obj.Scale = _scale;
            SetPosition(obj, _spawnArea);

            var b = StartCoroutine(Delay(obj, _lifeDelay));
            obj.Delay = b;
            _coroutines.Add(b);

            _time = 0;
        }
    }

    public int[] UpdateData()
    {
        int[] ints = new int[] { _session.Health, _session.Score, _session.Money, _session.Destroyed, _pool.Count };
        Updated?.Invoke();
        return ints;
    }

    private IEnumerator Delay(Entity entity, float lifeDelay)
    {
        yield return new WaitForSeconds(lifeDelay);

        StartCoroutine(_entryPoint.VFXPlayer.Animate(entity, entity.IsDestroyed, true));
        print("Started to returning to pool from itself");
        StartCoroutine(ReturnEntity(entity, CatAttacked));
    }

    public void OnHitted(IHitable hitable)
    {
        var hitted = (Entity)hitable;
        hitted.Hit();

        StopCoroutine(hitted.Delay);

        StartCoroutine(_entryPoint.VFXPlayer.Animate(hitted, hitted.IsDestroyed, true));
        print("Started to returning to pool from clicking");
        StartCoroutine(ReturnEntity(hitted, CatWasDestroyed));
    }

    public IEnumerator ReturnEntity(Entity entity, Action action)
    {
        yield return new WaitUntil(() => entity.IsAnimationEnded == true);
        _pool.Return(entity);
        action?.Invoke();
    }

    public void SetScale(Entity entity, float scale)
    {
        var width = entity.EntityConfig.StartSprite.rect.width;
        var height = entity.EntityConfig.StartSprite.rect.height;
        var pixels = entity.EntityConfig.StartSprite.pixelsPerUnit;

        entity.gameObject.transform.localScale = new Vector2((scale * pixels) / width, (scale * pixels) / height);

        entity.BoxCollider2D.size = new Vector2(width / pixels, height / pixels);

    }

    public void SetPosition(Entity entity, RectTransform spawnArea)
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

        System.Random random = new();
        var hor = random.NextDouble();
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

    private void OnEnable()
    {
        _entryPoint.ClickHandler.Hitted += OnHitted;
    }

    private void OnDisable()
    {
        _entryPoint.ClickHandler.Hitted -= OnHitted;
    }
}

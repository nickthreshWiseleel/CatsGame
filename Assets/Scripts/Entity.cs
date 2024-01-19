using UnityEditor;
using UnityEngine;

public class Entity : MonoBehaviour, IHitable, ICancelableDelay, IScalable, IAnimatable
{
    private EntityConfig _entityConfig;
    private BoxCollider2D _boxCollider2D;
    private Animator _animator;
    private readonly string _isDestroyed = "isDestroyed";
    private bool _isAnimationEnded;

    public Animator Animator => _animator;
    public EntityConfig EntityConfig => _entityConfig;
    public BoxCollider2D BoxCollider2D => _boxCollider2D;
    public Sprite CurrentSprite
    {
        get
        {
            return GetComponent<SpriteRenderer>().sprite;
        }
        private set
        {
            GetComponent<SpriteRenderer>().sprite = value;
        }
    }
    public Coroutine Delay { get; set; }
    public float Scale { get; set; }
    public bool IsIdle { get; set; }
    public string IsDestroyed => _isDestroyed;
    public bool IsAnimationEnded => _isAnimationEnded;

    private void Awake()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
    }


    public void SetScale()
    {
        var width = CurrentSprite.rect.width;
        var height = CurrentSprite.rect.height;
        var pixels = CurrentSprite.pixelsPerUnit;

        transform.localScale = new Vector2((Scale * pixels) / width, (Scale * pixels) / height);
        //transform.localScale = new Vector2((pixels) / width, (pixels) / height);
    }

    public void InitConfig(EntityConfig entityConfig)
    {
        _entityConfig = entityConfig;
    }

    public void Hit()
    {
        _boxCollider2D.enabled = false;
    }

    public void OnAnimationStart()
    {
        _isAnimationEnded = false;
        CurrentSprite = _entityConfig.StartSprite;
    }

    public void OnAnimationEnd()
    {
        _isAnimationEnded = true;
    }

    private void OnDisable()
    {
        _boxCollider2D.enabled = true;
    }
}
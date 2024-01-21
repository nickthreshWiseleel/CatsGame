using UnityEngine;

public class Explosion : MonoBehaviour, IVFX
{
    private Animator _animator;
    private bool _isAnimationEnded;
    private Sprite _sprite;

    public Animator Animator => _animator;
    public Sprite Sprite => _sprite;
    public bool IsAnimationEnded => _isAnimationEnded;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>().sprite;
    }

    public void Play()
    {

    }

    public void StartAnimation()
    {
        _isAnimationEnded = false;
    }

    public void EndAnimation()
    {
        _isAnimationEnded = true;
        Ended?.Invoke();
    }
}

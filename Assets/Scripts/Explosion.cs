using System;
using UnityEngine;

public class Explosion : MonoBehaviour, IVFX
{
    private Animator _animator;
    private bool _isAnimationEnded;
    private Sprite _sprite;
    private const string _isDestroyed = "isDestroyed";
    private Action<Explosion> _ended;

    public Animator Animator => _animator;
    public Sprite Sprite => _sprite;
    public bool IsAnimationEnded => _isAnimationEnded;

    public event Action Ended;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>().sprite;
    }

    public void Play()
    {
        _animator.SetBool(_isDestroyed, true);

    }

    public void AnimationEnded()
    {
        _ended?.Invoke(this);
    }

    public Explosion OnAnimationEnded(Action<Explosion> ended)
    {
        _ended = ended;
        return this;
    }

    private void OnDisable()
    {
        _animator.SetBool(_isDestroyed, false);
    }
}

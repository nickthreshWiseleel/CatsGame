using System;
using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour, IHitable
{
    private BoxCollider2D _boxCollider2D;
    private Coroutine _delay;
    private Action<Entity> _destroyed;
    private Action<Entity> _hitted;

    private void Awake()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private IEnumerator Delay(float lifeDelay)
    {
        yield return new WaitForSeconds(lifeDelay);

        _destroyed?.Invoke(this);
    }

    public Entity OnDestroyed(Action<Entity> action) //rename method
    {
        _destroyed = action;
        return this;
    }

    public Entity OnHitted(Action<Entity> action) //rename method
    {
        _hitted = action;
        return this;
    }

    public void Init(float lifeDelay)
    {
        _delay = StartCoroutine(Delay(lifeDelay));
    }

    public void Hit()
    {
        _boxCollider2D.enabled = false;

        StopCoroutine(_delay);

        _hitted?.Invoke(this);
    }

    private void OnDisable()
    {
        _boxCollider2D.enabled = true;
    }
}
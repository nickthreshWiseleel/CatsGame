using System;
using UnityEngine;

public class ExplosionFactory<Explosion> : IFactory<Explosion>
{

}

public class Factory<T> : IFactory<T>
{
    private T _entity;
    private float _scale;
    private float _lifetime;
    private Sprite _sprite;

    public Factory(T entity, float scale, float lifetime)
    {
        _entity = entity;
        _scale = scale;
        _lifetime = lifetime;
    }

    public Factory(T entity, float scale, Sprite sprite)
    {
        _entity = entity;
        _scale = scale;
        _sprite = sprite;
    }

    public T Create()
    {
        var type = typeof(T);
        if (type is Entity && _entity is Entity)
        {
            var entity = _entity as Entity;
            var created = GameObject.Instantiate(entity);
            created.name = entity.gameObject.name;
            SetScale(created.gameObject, created.Sprite, _scale);
            created.Init(_lifetime);
            return (T)Convert.ChangeType(created, typeof(T));
        }
        else
        {
            var entity = _entity as GameObject;
            var created = GameObject.Instantiate(entity);
            created.name = entity.name;
            SetScale(created, _sprite, _scale);
            return (T)Convert.ChangeType(created, typeof(T));
        }
    }

    private void SetScale(GameObject gameObject, Sprite sprite, float scale)
    {
        var width = sprite.rect.width;
        var height = sprite.rect.height;
        var pixels = sprite.pixelsPerUnit;

        gameObject.transform.localScale = new Vector2((scale * pixels) / width, (scale * pixels) / height);
    }
}

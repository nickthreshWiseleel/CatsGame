using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class Pool<T>
{
    List<T> _list = new List<T>();
    IFactory<T> _factory;

    public int Count => _list.Count;

    public Pool(IFactory<T> factory)
    {
        _factory = factory;
    }

    public T Get(T element)
    {
        if (_list.Count <= 0)
        {
            Create(element);
        }

        element = _list.LastOrDefault();
        _list.Remove(element);
        TrySetActivity(element, true);
        return element;
    }

    public void Return(T element)
    {
        _list.Add(element);
        TrySetActivity(element, false);
    }

    private void TrySetActivity(T element, bool value)
    {
        if (element is Component component)
            component.gameObject.SetActive(value);

        if (element is GameObject gameObject)
            gameObject.SetActive(value);
    }

    private void Create(T element)
    {
        element = _factory.Create();
        _list.Add(element);
        TrySetActivity(element, false);
    }
}

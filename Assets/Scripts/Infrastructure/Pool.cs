using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Infrastructure
{
    public class Pool<T>
    {
        private readonly List<T> _list = new();
        private readonly IFactory<T> _factory;

        public int Count => _list.Count;

        public Pool(IFactory<T> factory)
        {
            _factory = factory;
        }

        public T Get()
        {
            T element;

            if (_list.Count <= 0)
            {
                element = _factory.Create();
                _list.Add(element);
                SetActivity(element, false);
            }

            element = _list.LastOrDefault();
            _list.Remove(element);
            SetActivity(element, true);
            return element;
        }

        public void Return(T element)
        {
            _list.Add(element);
            SetActivity(element, false);
        }

        private void SetActivity(T element, bool value)
        {
            if (element is Component component)
                component.gameObject.SetActive(value);

            if (element is GameObject gameObject)
                gameObject.SetActive(value);
        }
    }
}
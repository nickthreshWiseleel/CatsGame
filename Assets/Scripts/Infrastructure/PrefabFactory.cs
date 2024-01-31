using UnityEngine;

namespace Infrastructure
{
    public class PrefabFactory<T> : IFactory<T> where T : Component
    {
        private readonly T _prefab;

        public PrefabFactory(T prefab)
        {
            _prefab = prefab;
        }

        public T Create()
        {
            var created = Object.Instantiate(_prefab);
            return created;
        }
    }
}
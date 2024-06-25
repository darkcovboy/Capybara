using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Pool
{
    public abstract class ObjectPool<T> : MonoBehaviour where T : Component
    {
        [SerializeField] private T _prefab;
        [SerializeField] private int _initialSize = 10;

        private readonly List<T> _objects = new List<T>();

        protected virtual void Awake()
        {
            for (int i = 0; i < _initialSize; i++)
            {
                T newObject = CreateNewObject();
                _objects.Add(newObject);
            }
        }

        public T Get()
        {
            T obj = _objects.FirstOrDefault(o => o.gameObject.activeSelf == false);
            
            if (obj != null)
            {
                return obj;
            }

            Debug.LogWarning("No inactive objects available in the pool.");
            return null;

        }

        private T CreateNewObject()
        {
            T newObj = Instantiate(_prefab, transform);
            newObj.gameObject.SetActive(false);
            return newObj;
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private int _capacity;

    private readonly List<T> _pool = new List<T>();

    public IReadOnlyCollection<T> Pool => _pool;

    public int Capacity => _capacity;

    protected void Initialize(T prefab)
    {
        for (int i = 0; i < _capacity; i++)
        {
            T spawned = Instantiate(prefab, _container.transform);
            spawned.gameObject.SetActive(false);
            spawned.name += i;

            _pool.Add(spawned);
        }
    }

    protected bool TryGetObject(out T result)
    {
        result = _pool.FirstOrDefault(p => p.gameObject.activeSelf == false);
        _pool.Remove(result);

        return result != null;
    }
}

using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private int _capacity;

    protected List<GameObject> Pool = new List<GameObject>();

    protected void Initialize(GameObject prefab)
    {
        for (var i = 0; i < _capacity; i++)
        {
            var spawned = Instantiate(prefab, _container.transform);
            Pool.Add(spawned);
        }
    }

    protected bool TryGetObject(out GameObject result)
    {
        result = Pool.FirstOrDefault(p => p.activeSelf == false);

        return result != null;
    }
}


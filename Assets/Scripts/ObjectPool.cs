using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] protected int _capacity;

    protected readonly List<GameObject> Pool = new List<GameObject>();

    protected void Initialize(GameObject prefab)
    {
        for (var i = 0; i < _capacity; i++)
        {
            var spawned = Instantiate(prefab, _container.transform);
            spawned.SetActive(false);
            Pool.Add(spawned);
        }
    }
}


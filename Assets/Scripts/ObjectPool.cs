using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] protected int _capacity;

    protected readonly List<GameObject> Pool = new List<GameObject>();

    protected void Initialize(GameObject template)
    {
        for (var i = 0; i < _capacity; i++)
        {
            var spawned = Instantiate(template, _container.transform);
            Pool.Add(spawned);
        }
    }
}


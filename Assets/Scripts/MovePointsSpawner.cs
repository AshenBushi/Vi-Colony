using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class MovePointsSpawner : ObjectPool
{
    [SerializeField] private GameObject _template;
    private int _index = -1;

    public int Index => _index;

    private void Awake()
    {
        Initialize(_template);

        float previousPointY = 0;
        
        foreach (var point in Pool)
        {
            point.transform.position = new Vector2(2 + 2 * Random.Range(-2.5f, 2.5f),previousPointY + Random.Range(5f, 8f));
            previousPointY = point.transform.position.y;
        }
    }

    public void NextIndex(ref int index)
    {
        if (index < Pool.Count - 1)
            index++;
        else
            index = 0;
    }

    public Transform GetMovingPoint(int index)
    {
        return Pool[index].transform;
    }
    
    public GameObject GetNextMovingPoint()
    {
        if(_index >= 0)
            Pool[_index].SetActive(false);
        NextIndex(ref _index);
        return Pool[_index];
    }

    public void SpawnNewMovingPoint()
    {
        var pointIndex = _index;
        
        NextIndex(ref pointIndex);
        
        var pointPosition = Pool[pointIndex].transform.position;

        NextIndex(ref pointIndex);
        Pool[pointIndex].transform.position = new Vector2(2 + 2 * Random.Range(-2.5f, 2.5f), pointPosition.y + Random.Range(5f, 8f));
        Pool[pointIndex].gameObject.SetActive(true);
    }
}

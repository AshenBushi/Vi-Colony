using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleSpawner: ObjectPool
{
    [SerializeField] private GameObject _template;
    [SerializeField] private int _radius;

    private List<ObstacleMover> _obstacles = new List<ObstacleMover>();
    private float _speed;
    private float _startAngle;
    private float _angleStep;
    private int _toTurnOn;

    private void Awake()
    {
        _capacity = 5 * _radius;
        _angleStep = 6.28f / _capacity;
        Initialize(_template);
        
        foreach (var item in Pool)
        {
            _obstacles.Add(item.GetComponent<ObstacleMover>());
            item.SetActive(false);
        }
    }

    public void DisableAllObstacles()
    {
        foreach (var obstacle in _obstacles)
        {
            obstacle.gameObject.SetActive(false);
        }
    }

    private void Generator(int pointNumber)
    {
        var level = pointNumber / 50;

        switch (level)
        {
            case 0:
                _toTurnOn = Random.Range(Convert.ToInt32(2 + pointNumber / 10), Convert.ToInt32(5 + pointNumber / 10));
                _speed = Random.Range(1, 3) + pointNumber / 15;
                break;
            case 1:
                _toTurnOn = Random.Range(_capacity / 2, _capacity - 2);
                _speed = Random.Range(1, 3) + level;
                break;
            default:
                _toTurnOn = Random.Range(2, 9);
                _speed = Random.Range(1, 5);
                break;
        }
        
    }
    
    public void SpawnObstacles(int pointNumber)
    {
        DisableAllObstacles();
        Generator(pointNumber);

        _startAngle = 0;
        
        foreach (var obstacle in _obstacles)
        {
            obstacle.SetParameters(_radius, _speed, _startAngle);
            _startAngle += _angleStep;
        }
        
        while (_toTurnOn > 0)
        {
            var index = Random.Range(0, _obstacles.Count);
            if (_obstacles[index].gameObject.activeSelf != false) continue;
            _obstacles[index].gameObject.SetActive(true);
            _toTurnOn--;
        }
    }
}

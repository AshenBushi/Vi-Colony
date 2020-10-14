using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleSpawner: ObjectPool
{
    [SerializeField] private GameObject _template;
    [SerializeField] private int _radius;

    private List<Obstacle> _obstacles = new List<Obstacle>();
    private float _speed;
    private float _angle;
    private float _angleStep;

    private void Awake()
    {
        _capacity = 5 * _radius;
        _angleStep = 6.28f / _capacity;
        Initialize(_template);
        
        foreach (var item in Pool)
        {
            _obstacles.Add(item.GetComponent<Obstacle>());
        }
    }

    public void DisableAllObstacles()
    {
        foreach (var obstacle in _obstacles)
        {
            obstacle.gameObject.SetActive(false);
        }
    }
    
    public void GenerateObstaclesCombination(int pointNumber)
    {
        foreach (var obstacle in _obstacles)
        {
            obstacle.gameObject.SetActive(true);
        }
        
        var level = pointNumber / 50;
        int obstaclesToTurnOff;

        switch (level)
        {
            case 0:
                obstaclesToTurnOff = Random.Range(_capacity / 2, _capacity - 4);
                _speed = Random.Range(1, 4);
                break;
            default:
                obstaclesToTurnOff = Random.Range(_capacity / 2, _capacity - 4);
                _speed = Random.Range(1, 3);
                break;
        }

        _angle = 0;
        
        foreach (var obstacle in _obstacles)
        {
            obstacle.SetParameters(_radius, _speed, _angle);
            _angle += _angleStep;
        }
        
        while (obstaclesToTurnOff > 0)
        {
            var index = Random.Range(0, _obstacles.Count);
            if (_obstacles[index].gameObject.activeSelf != true) continue;
            _obstacles[index].gameObject.SetActive(false);
            obstaclesToTurnOff--;
        }
    }
}

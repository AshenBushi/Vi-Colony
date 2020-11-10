using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleSpawner: ObjectPool
{
    [SerializeField] private GameObject _template;
    [SerializeField] private int _radius;

    private readonly List<ObstacleMover> _obstacleMovers = new List<ObstacleMover>();
    private float _speed;
    private float _startAngle;
    private float _angleStep;
    private int _toTurnOn;
    private int _level;
    private int _pointNumber;
    private bool _direction;

    private void Awake()
    {
        _capacity = 5 * _radius;
        _angleStep = 6.28f / _capacity;
        Initialize(_template);
        
        foreach (var item in Pool)
        {
            _obstacleMovers.Add(item.GetComponent<ObstacleMover>());
        }
    }

    public void DisableAllObstacles(int playAnimNumber)
    {
        switch (playAnimNumber)
        {
            case 0:
                foreach (var obstacle in _obstacleMovers)
                    obstacle.DisableObstacle();
                break;
            default:
                foreach (var obstacle in _obstacleMovers)
                    obstacle.gameObject.SetActive(false);
                break;
        }
    }

    private void Generator(int number)
    {
        _level = number / 25;
        _pointNumber = number - _level * 25;
        
        _toTurnOn = Random.Range(Convert.ToInt32(_capacity / 5 + _pointNumber / 5), Convert.ToInt32(_capacity / 2 + _pointNumber / 5));
        _speed = 1 + number / 25;

        var chance = Random.Range(0, 100);

        _direction = chance < 50;
    }
    
    public void SpawnObstacles(int pointNumber)
    {
        Generator(pointNumber);

        _startAngle = 0;
        
        foreach (var obstacle in _obstacleMovers)
        {
            obstacle.SetParameters(_radius, _speed, _startAngle, _direction);
            _startAngle += _angleStep;
        }
        
        while (_toTurnOn > 0)
        {
            var index = Random.Range(0, _obstacleMovers.Count);
            if (_obstacleMovers[index].gameObject.activeSelf != false) continue;
            _obstacleMovers[index].EnableObstacle();
            _toTurnOn--;
        }
    }
}

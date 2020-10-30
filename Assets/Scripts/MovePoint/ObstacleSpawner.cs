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
            _obstacles.Add(item.GetComponent<ObstacleMover>());
        }
        
        DisableAllObstacles(false);
    }

    public void DisableAllObstacles(bool playAnim)
    {
        if (playAnim)
        {
            foreach (var obstacle in _obstacles)
            {
                obstacle.DisableObstacle();
            }
        }
        else
        {
            foreach (var obstacle in _obstacles)
                obstacle.gameObject.SetActive(false);
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

        /*switch (_level)
        {
            case 0:
                _toTurnOn = Random.Range(Convert.ToInt32(_capacity / 5 + _pointNumber / 10), Convert.ToInt32(_capacity / 2 + _pointNumber / 10));
                _speed = 1 + _pointNumber / 50;
                break;
            case 1:
                _toTurnOn = Random.Range(Convert.ToInt32(_capacity / 5 + _pointNumber / 10), Convert.ToInt32(_capacity / 2 + _pointNumber / 10));
                _speed = 3 + _pointNumber / 50;
                break;
            default:
                _toTurnOn = Random.Range(Convert.ToInt32(_capacity / 5 + _pointNumber / 10), Convert.ToInt32(_capacity / 2 + _pointNumber / 10));
                _speed = 5 + _pointNumber / 50;
                break;
        }*/
    }
    
    public void SpawnObstacles(int pointNumber)
    {
        DisableAllObstacles(false);
        Generator(pointNumber);

        _startAngle = 0;
        
        foreach (var obstacle in _obstacles)
        {
            obstacle.SetParameters(_radius, _speed, _startAngle, _direction);
            _startAngle += _angleStep;
        }
        
        while (_toTurnOn > 0)
        {
            var index = Random.Range(0, _obstacles.Count);
            if (_obstacles[index].gameObject.activeSelf != false) continue;
            _obstacles[index].gameObject.SetActive(true);
            _obstacles[index].EnableObstacle();
            _toTurnOn--;
        }
    }
}

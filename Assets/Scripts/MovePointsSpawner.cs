using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class MovePointsSpawner : ObjectPool
{
    [SerializeField] private GameObject _template;
    [SerializeField] private PlayerMover _playerMover;

    private int _index = -1;
    private int _pointNumber = 0;
    
    public List<MovePoint> MovePoints { get; } = new List<MovePoint>();
    public int Index => _index;

    private void Awake()
    {
        Initialize(_template);

        foreach (var point in Pool)
        {
            MovePoints.Add(point.GetComponent<MovePoint>());
        }
    }

    private void Start()
    {
        float previousPointY = 0;
        
        foreach (var point in MovePoints)
        {
            SetPointPosition(point, previousPointY);
            previousPointY = point.transform.position.y;
        }
    }

    private void OnEnable()
    {
        _playerMover.MakeJump += SpawnNewMovingPoint;
    }

    private void OnDisable()
    {
        _playerMover.MakeJump -= SpawnNewMovingPoint;
    }

    public void NextIndex(ref int index)
    {
        if (index < MovePoints.Count - 1)
            index++;
        else
            index = 0;
    }

    public MovePoint GetNextMovingPoint()
    {
        if(_index >= 0)
            MovePoints[_index].gameObject.SetActive(false);
        NextIndex(ref _index);
        return MovePoints[_index];
    }

    private void SpawnNewMovingPoint()
    {
        var pointIndex = _index;
        
        foreach (var item in MovePoints[pointIndex].GetComponentsInChildren<ObstacleSpawner>())
        {
            item.DisableAllObstacles();
        }
        
        NextIndex(ref pointIndex);
        
        var pointPosition = MovePoints[pointIndex].transform.position;

        NextIndex(ref pointIndex);
        SetPointPosition(MovePoints[pointIndex], pointPosition.y);
        MovePoints[pointIndex].gameObject.SetActive(true);
    }

    private void SetPointPosition(MovePoint point, float previousPointY)
    {
        point.transform.position = new Vector2(Random.Range(-3.5f, 3.5f),previousPointY + Random.Range(5f, 8f));

        foreach (var item in point.GetComponentsInChildren<ObstacleSpawner>())
        {
            item.SpawnObstacles(_pointNumber++);
        }
    }
}

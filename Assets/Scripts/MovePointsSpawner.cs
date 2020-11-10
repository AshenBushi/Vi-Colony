using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MovePointsSpawner : ObjectPool
{
    [SerializeField] private GameObject _template;

    private readonly List<MovePoint> _movePoints = new List<MovePoint>();
    private MovePoint _lastSpawnedPoint;
    private int _pointNumber = 1;

    private void Awake()
    {
        Initialize(_template);

        foreach (var item in Pool)
        {
            _movePoints.Add(item.GetComponent<MovePoint>());
        }
    }
    
    private void Start()
    {
        for (var i = 0; i < _movePoints.Count - 1; i++)
        {
            SpawnMovePoint();
        }
    }
    
    public void SpawnMovePoint()
    {
        if (!TryGetObject(out var movePoint)) return;
        
        var x = Random.Range(-3.5f, 3.5f);
        var y = (_lastSpawnedPoint != null ? _lastSpawnedPoint.transform.position.y : 0) + Random.Range(7f, 8f);
        
        movePoint.Spawn(x, y, _pointNumber);
        
        _pointNumber++;
            
        _lastSpawnedPoint = movePoint;
    }

    private bool TryGetObject(out MovePoint result)
    {
        result = _movePoints.FirstOrDefault(p => p.gameObject.activeSelf == false);

        return result != null;
    }

    public MovePoint GiveMovePoint(int number)
    {
        var result = _movePoints.First(point => point.Number == number);
            
        return result;
    }

    public List<MovePoint> GiveMovePoints()
    {
        return _movePoints;
    }

    public void TryDisableMovePoint(int number)
    {
        var result = _movePoints.First(point => point.Number == number);
        
        if(result != null)
            result.gameObject.SetActive(false);
    }
}

using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class SceneCalibrator : MonoBehaviour
{
    [SerializeField] private PlayerMover _player;
    [SerializeField] private MovePointsSpawner _movePointsSpawner;

    private Tween _moveTween;
    private List<MovePoint> _movePoints = new List<MovePoint>();
    private Vector2 _playerCalibratedPosition;
    private readonly Vector2[] _pointsCalibratedPosition = new Vector2[3];
    private int _pointIndex;
    public float Duration { get; } = 0.4f;

    public event UnityAction OnCaliber;

    private void Start()
    {
        _movePoints = _movePointsSpawner.MovePoints;
    }

    public void CalibrateScene()
    {
        SetCalibratedPositions();
        
        OnCaliber?.Invoke();
        _moveTween = _player.transform.DOMove(_playerCalibratedPosition, Duration).SetLink(gameObject);

        for (var i = 0; i < _movePoints.Count; i++)
        {
            _moveTween = _movePoints[i].transform.DOMove(_pointsCalibratedPosition[i], Duration).SetLink(gameObject);
        }

        _moveTween.OnComplete(_player.EnableTapping);
    }

    private void SetCalibratedPositions()
    {
        _pointIndex = _movePointsSpawner.Index;
        var playerPosition = _player.transform.position;
        var yCelebrateStep = _movePoints[_pointIndex].transform.position.y;
        var saveIndex = _pointIndex;
        _movePointsSpawner.NextIndex(ref _pointIndex);

        var celebrateValue = Mathf.Abs(playerPosition.x - _movePoints[_pointIndex].transform.position.x) / 2;
        var celebrateNumber = playerPosition.x > _movePoints[_pointIndex].transform.position.x ? 1 : -1;

        _playerCalibratedPosition = new Vector2(0 + celebrateValue * celebrateNumber, playerPosition.y - yCelebrateStep);
        _pointsCalibratedPosition[_pointIndex] = new Vector2(0 - celebrateValue * celebrateNumber, _movePoints[_pointIndex].transform.position.y - yCelebrateStep);
        
        for (var i = 0; i < _movePoints.Count; i++)
        {
            if (i == _pointIndex) continue;
            if(i == saveIndex)
                _pointsCalibratedPosition[i] = _playerCalibratedPosition;
            else
                _pointsCalibratedPosition[i] = new Vector2(_movePoints[i].transform.position.x, _movePoints[i].transform.position.y - yCelebrateStep);
        }
    }
}

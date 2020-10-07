using DG.Tweening;
using UnityEngine;

public class SceneCallibler : MonoBehaviour
{
    [SerializeField] private PlayerMover player;
    [SerializeField] private MovingPointsPool movingPointsPool;

    private Tween _moveTween;
    private readonly Transform[] _movingPoints = new Transform[4];
    private Vector2 _playerCalibratedPosition;
    private readonly Vector2[] _pointsCalibratedPosition = new Vector2[4];
    private int _pointIndex;
    private float _duration = 0.5f;

    private void Start()
    {
        for (var i = 0; i < _movingPoints.Length; i++)
        {
            _movingPoints[i] = movingPointsPool.GetMovingPoint(i);
        }
    }

    public void CalibrateScene()
    {
        SetCalibratedPositions();
        
        _moveTween = player.transform.DOMove(_playerCalibratedPosition, _duration);

        for (var i = 0; i < _movingPoints.Length; i++)
        {
            _moveTween = _movingPoints[i].transform.DOMove(_pointsCalibratedPosition[i], _duration);
        }

        _moveTween.OnComplete(player.EnableTapping);
    }

    private void SetCalibratedPositions()
    {
        _pointIndex = movingPointsPool.Index;
        var playerPosition = player.transform.position;
        var yCelebrateStep = _movingPoints[_pointIndex].position.y;
        var saveIndex = _pointIndex;
        movingPointsPool.NextIndex(ref _pointIndex);

        var celebrateValue = Mathf.Abs(playerPosition.x - _movingPoints[_pointIndex].position.x) / 2;
        var celebrateNumber = playerPosition.x > _movingPoints[_pointIndex].position.x ? 1 : -1;

        _playerCalibratedPosition = new Vector2(2 + celebrateValue * celebrateNumber, playerPosition.y - yCelebrateStep);
        _pointsCalibratedPosition[_pointIndex] = new Vector2(2 - celebrateValue * celebrateNumber, _movingPoints[_pointIndex].position.y - yCelebrateStep);
        
        for (var i = 0; i < _movingPoints.Length; i++)
        {
            if (i == _pointIndex) continue;
            if(i == saveIndex)
                _pointsCalibratedPosition[i] = _playerCalibratedPosition;
            else
                _pointsCalibratedPosition[i] = new Vector2(_movingPoints[i].position.x, _movingPoints[i].position.y - yCelebrateStep);
        }
    }
}

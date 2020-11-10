using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class SceneCalibrator : MonoBehaviour
{
    public float Duration { get; } = 0.4f;
    
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private MovePointsSpawner _movePointsSpawner;

    private Tween _tween;
    private List<MovePoint> _movePoints = new List<MovePoint>();

    public event UnityAction OnCaliber;

    private void Start()
    {
        _movePoints = _movePointsSpawner.GiveMovePoints();
    }

    public void CalibrateScene()
    {
        var playerPosition = _playerMover.transform.position;
        var yCalibrate = _movePointsSpawner.GiveMovePoint(_playerMover.JumpsCount).transform.position.y;
        var nextPointPosition = _movePointsSpawner.GiveMovePoint(_playerMover.JumpsCount + 1).transform.position;
        var xCalibrate = Mathf.Abs(playerPosition.x - nextPointPosition.x) / 2 * (playerPosition.x > nextPointPosition.x ? 1: -1);
        var playerCalibratedPosition = new Vector2(0 + xCalibrate, playerPosition.y - yCalibrate);
        
        OnCaliber?.Invoke();
            
        _tween = _playerMover.transform.DOMove(playerCalibratedPosition, Duration).SetLink(gameObject);
        foreach (var movePoint in _movePoints)
        {
            var position = movePoint.transform.position;
            
            if (movePoint.Number == _playerMover.JumpsCount + 1)
                _tween = movePoint.transform.DOMove(new Vector2(0 - xCalibrate, position.y - yCalibrate), Duration);
            else if (movePoint.Number == _playerMover.JumpsCount)
                _tween = movePoint.transform.DOMove(playerCalibratedPosition, Duration);
            else
                _tween = movePoint.transform.DOMove(new Vector2(position.x, position.y - yCalibrate), Duration);
        }
        
        _tween.OnComplete(() =>
        {
            _movePointsSpawner.TryDisableMovePoint(_playerMover.JumpsCount - 1);
            _playerMover.EnableTap();
            _movePointsSpawner.SpawnMovePoint();
        });
    }
}

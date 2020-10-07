using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private MovingPointsPool movingPointsPool;
    private Player _player;
    private Tween _moveTween;
    private GameObject _currentMovingPoint;
    private void Start()
    {
        _player = GetComponent<Player>();
    }
    private void Update()
    {
        if (!Input.GetMouseButtonDown(0) || _moveTween != null) return;
        _moveTween.SetEase(Ease.Linear);
        _moveTween = transform.DOMove(movingPointsPool.GetNextMovingPoint().transform.position,50 / _player.Speed);
        _moveTween.onComplete += () =>
        {
            movingPointsPool.DisableCurrentPoint();
            _moveTween = null;
        };
    }
}

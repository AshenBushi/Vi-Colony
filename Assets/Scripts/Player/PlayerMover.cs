using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private MovePointsSpawner _movePointsSpawner;
    [SerializeField] private SceneCalibrator _sceneCalibrator;

    private PlayerInput _input;
    private Player _player;
    private Tween _moveTween;

    public event UnityAction MakeJump;

    private void Awake()
    {
        _input = new PlayerInput();
        _input.Enable();
        _input.Player.Tap.performed += ctx => OnTap();
    }

    private void OnEnable()
    {
        _player = GetComponent<Player>();
    }
    private void OnTap()
    {
        if (_moveTween != null) return;
        var spawnerPosition = _movePointsSpawner.GetNextMovingPoint().transform.position;
        _moveTween = transform.DOMove(spawnerPosition,(spawnerPosition.y - _player.transform.position.y) / (_player.Speed * 2)).SetEase(Ease.Linear).SetLink(gameObject);
        _moveTween.OnComplete(NextJump);
    }
    private void NextJump()
    {
        MakeJump?.Invoke();
        _sceneCalibrator.CalibrateScene();
    }

    public void EnableTapping()
    {
        _moveTween = null;
    }
}

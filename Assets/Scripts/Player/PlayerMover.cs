using System;
using System.Diagnostics.CodeAnalysis;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class PlayerMover : MonoBehaviour
{
    private const float _startSpeed = 100;
    
    [SerializeField] private MovePointsSpawner _movePointsSpawner;
    [SerializeField] private SceneCalibrator _sceneCalibrator;

    private Player _player;
    private AudioSource _audio;
    private PlayerInput _input;
    private Tween _tween;
    private Vector3 _lastPlayerPosition;
    private float _speed = _startSpeed;
    
    public int JumpsCount { get; private set; }
    
    public event UnityAction<int> OnJump;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _audio = GetComponent<AudioSource>();
        _input = new PlayerInput();
        _input.Player.Tap.performed += ctx => OnTap();
    }

    private void OnEnable()
    {
        _player.OnApplyDamage += ReturnToLastPoint;
    }

    private void OnDisable()
    {
        _player.OnApplyDamage -= ReturnToLastPoint;
    }
    
    private void OnTap()
    {
        var position = transform.position;
        var nextPointPosition = _movePointsSpawner.GiveMovePoint(JumpsCount + 1).transform.position;
        
        _audio.Play();
        _input.Disable();
        _lastPlayerPosition = position;
        _tween = transform
            .DOMove(nextPointPosition, (nextPointPosition.y - position.y) / (_speed * 2 / 10))
            .SetEase(Ease.Linear).SetLink(gameObject);
        _tween.OnComplete(() =>
        {
            JumpsCount++;
            OnJump?.Invoke(JumpsCount);
            IncreaseSpeed();
            _sceneCalibrator.CalibrateScene();   
        });
    }

    private void ReturnToLastPoint()
    {
        _tween.Kill();
        _tween = transform
            .DOMove(_lastPlayerPosition, (transform.position.y - _lastPlayerPosition.y) / (_speed * 2 / 10))
            .SetEase(Ease.Linear).SetLink(gameObject);
        _tween.OnComplete(() => _input.Enable());
    }
    
    private void IncreaseSpeed()
    {
        if(_speed < 150)
            _speed = _startSpeed + JumpsCount / 2.5f;
    }
    
    public void EnableTap()
    {
        _input.Enable();
    }
    
    public void DisableTap()
    {
        _input.Disable();
    }
}

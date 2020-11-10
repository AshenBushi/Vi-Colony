using System;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class PlayerMover : Player
{
    [SerializeField] private MovePointsSpawner _movePointsSpawner;
    [SerializeField] private SceneCalibrator _sceneCalibrator;

    private Vector3 _lastPlayerPosition;
    private PlayerInput _input;
    private Tween _tween;
    private AudioSource _audio;
    private float _playedTime;

    public event UnityAction<int> OnJump;

    private void Awake()
    {
        PlayerCollider = GetComponent<CircleCollider2D>();
        _audio = GetComponent<AudioSource>();
        _input = new PlayerInput();
        _input.Player.Tap.performed += ctx => OnTap();
    }

    private void OnEnable()
    {
        OnTakeDamage += ReturnToLastPoint;
    }

    private void OnDisable()
    {
        OnTakeDamage -= ReturnToLastPoint;
    }

    private void OnTap()
    {
        _audio.Play();
        _input.Disable();
        _lastPlayerPosition = transform.position;
        var nextPointPosition = _movePointsSpawner.GiveMovePoint(JumpsCount + 1).transform.position;
        _tween = transform
            .DOMove(nextPointPosition, (nextPointPosition.y - transform.position.y) / (_speed * 2 / 10))
            .SetEase(Ease.Linear).SetLink(gameObject); 
        _tween.OnComplete(CompleteJump);
    }
    private void CompleteJump()
    {
        JumpsCount++;
        OnJump?.Invoke(JumpsCount);
        IncreaseSpeed();
        _sceneCalibrator.CalibrateScene();
    }

    private void ReturnToLastPoint()
    {
        _tween.Kill();
        _tween = transform
            .DOMove(_lastPlayerPosition, (transform.position.y - _lastPlayerPosition.y) / (_speed * 2 / 10))
            .SetEase(Ease.Linear).SetLink(gameObject);
        _tween.OnComplete(() =>
        {
            EnableTap();
            PlayerCollider.enabled = true;
        });
    }
    
    public void EnableTap()
    {
        _input.Enable();
    }

    public void Died()
    {
        _input.Disable();
        _tween.Kill();
        ChangePlayerState(false);
    }

    public void Revive()
    {
        _health = 100;
        EnableTap();
        ChangePlayerState(true);
        transform.position = _lastPlayerPosition;
    }
    
    private void ChangePlayerState(bool state)
    {
        gameObject.SetActive(state);
        IsAlive = state;
    }
}

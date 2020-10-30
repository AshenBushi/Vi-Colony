using System;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private MovePointsSpawner _movePointsSpawner;
    [SerializeField] private SceneCalibrator _sceneCalibrator;

    private PlayerInput _input;
    private Player _player;
    private Tween _moveTween;
    private AudioSource _audio;
    private bool _isLose = false;

    public event UnityAction MakeJump;

    private void Awake()
    {
        _input = new PlayerInput();
        _input.Player.Tap.performed += ctx => OnTap();
    }


    private void OnEnable()
    {
        _player = GetComponent<Player>();
        _audio = GetComponent<AudioSource>();
    }

    private void OnTap()
    {
        if (_isLose) return;
        _audio.Play();
        _input.Disable();
        var spawnerPosition = _movePointsSpawner.GetNextMovingPoint().transform.position;
        _moveTween = transform
            .DOMove(spawnerPosition, (spawnerPosition.y - _player.transform.position.y) / (_player.Speed * 2))
            .SetEase(Ease.Linear).SetLink(gameObject);
        _moveTween.OnComplete(NextJump);
    }
    private void NextJump()
    {
        MakeJump?.Invoke();
        _sceneCalibrator.CalibrateScene();
    }

    public void EnableTapping()
    {
        _input.Enable();
    }
    
    public void Losing()
    {
        _isLose = true;
    }

    public void Continue()
    {
        _isLose = false;
    }
}

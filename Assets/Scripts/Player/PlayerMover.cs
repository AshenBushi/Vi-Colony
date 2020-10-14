using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private MovePointsSpawner _movePointsSpawner;
    [SerializeField] private SceneCallibler _sceneCallibler;

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
    private void Start()
    {
        _player = GetComponent<Player>();
    }
    private void OnTap()
    {
        if (_moveTween != null) return;
        _moveTween = transform.DOMove(_movePointsSpawner.GetNextMovingPoint().transform.position,50 / _player.Speed);
        _moveTween.OnComplete(NextJump);
    }
    private void NextJump()
    {
        MakeJump?.Invoke();
        _sceneCallibler.CalibrateScene();
    }

    public void EnableTapping()
    {
        _moveTween = null;
    }
}

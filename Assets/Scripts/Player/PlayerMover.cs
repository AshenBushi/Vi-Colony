using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private MovingPointsPool movingPointsPool;
    [SerializeField] private SceneCallibler sceneCallibler;
    private PlayerInput _input;
    private Player _player;
    private Tween _moveTween;
    private GameObject _currentMovingPoint;

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
        _moveTween = transform.DOMove(movingPointsPool.GetNextMovingPoint().transform.position,50 / _player.Speed);
        _moveTween.OnComplete(NextJump);
    }
    private void NextJump()
    {
        movingPointsPool.SpawnNewMovingPoint();
        sceneCallibler.CalibrateScene();
    }

    public void EnableTapping()
    {
        _moveTween = null;
    }
}

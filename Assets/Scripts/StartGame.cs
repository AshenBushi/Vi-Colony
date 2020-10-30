using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
public class StartGame : MonoBehaviour
{
    [SerializeField] private GameObject _spawner;
    [SerializeField] private GameObject _text;
    [SerializeField] private GameObject _score;

    private PlayerMover _mover;
    private PlayerInput _input;
    private Tween _tween;
    private void Awake()
    {
        _mover = GetComponent<PlayerMover>();
        _input = new PlayerInput();
        _input.Enable();
        _input.Player.Tap.performed += ctx => OnTap();
    }
    
    private void OnTap()
    {
        _input.Disable();
        _text.SetActive(false);
        
        _tween = transform.DOMove(new Vector3(0f, 0f, 0f), 0.5f).SetEase(Ease.Linear);
        transform.DOScale(new Vector3(0.3f, 0.3f, 0.3f), 0.5f).SetEase(Ease.Linear);
        _tween.OnComplete(EnableSpawner);
    }

    private void EnableSpawner()
    {
        _score.SetActive(true);
        _spawner.SetActive(true);
        _mover.EnableTapping();
        _tween.Kill();
    }
}

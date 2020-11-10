using DG.Tweening;
using UnityEngine;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private GameObject _spawner;
    [SerializeField] private GameObject _text;
    [SerializeField] private GameObject _score;
    [SerializeField] private PlayerMover _playerMover;

    private PlayerInput _input;
    private Tween _tween;
    private void Awake()
    {
        _input = new PlayerInput();
        _input.Enable();
        _input.Player.Tap.performed += ctx => OnTap();
    }
    
    private void OnTap()
    {
        _input.Disable();
        _text.SetActive(false);
        _tween = _playerMover.transform.DOMove(new Vector3(0f, 0f, 0f), 0.5f).SetEase(Ease.Linear);
        _playerMover.transform.DOScale(new Vector3(0.3f, 0.3f, 0.3f), 0.5f).SetEase(Ease.Linear);
        _tween.OnComplete(() => {
            _score.SetActive(true);
            _spawner.SetActive(true);
            _playerMover.EnableTap();
            _tween.Kill();});
    }
}

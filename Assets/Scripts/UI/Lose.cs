using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lose : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _score;
    [SerializeField] private GameObject _spawner;

    private CanvasGroup _canvasGroup;
    private PlayerMover _mover;

    private void OnEnable()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _mover = _player.GetComponent<PlayerMover>();
        _player.OnDie += OnDie;
    }

    private void OnDisable()
    {
        _player.OnDie -= OnDie;
    }

    private void OnDie()
    {
        _mover.Losing();
        _player.gameObject.SetActive(false);
        _score.SetActive(false);
        _spawner.SetActive(false);
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
    }

    private void StartLoseScreen()
    {
        
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void Continue()
    {
        _mover.Continue();
        _player.gameObject.SetActive(true);
        _score.SetActive(true);
        _spawner.SetActive(true);
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
    }
}

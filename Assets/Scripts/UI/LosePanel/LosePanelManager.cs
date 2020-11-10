using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LosePanelManager : MonoBehaviour
{
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private GameObject _score;
    [SerializeField] private GameObject _spawner;
    [SerializeField] private StatsManager _statsManager;

    private CanvasGroup _canvasGroup;

    private void OnEnable()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _playerMover.OnDied += OnDied;
    }

    private void OnDisable()
    {
        _playerMover.OnDied -= OnDied;
    }

    private void OnDied(int score, int dieCount)
    {
        _playerMover.Died();
        _score.SetActive(false);
        _spawner.SetActive(false);
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
        _statsManager.AppearStats(score, dieCount);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void Continue()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _score.SetActive(true);
        _spawner.SetActive(true);
        _playerMover.Revive();
        _statsManager.DisappearStats();
    }
}

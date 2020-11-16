using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class LosePanel : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private SortingGroup _background;
    [SerializeField] private StatsManager _statsManager;

    private CanvasGroup _canvasGroup;

    private void OnEnable()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _player.OnDied += OpenLosePanel;
    }

    private void OnDisable()
    {
        _player.OnDied -= OpenLosePanel;
    }

    private void OpenLosePanel()
    {
        _background.sortingOrder = 1;
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void Continue()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _player.Revive();
    }
}

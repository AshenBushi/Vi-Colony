using DG.Tweening;
using UnityEngine;

public class Tape : MonoBehaviour
{
    private const float EndY = -15f;
    private const float SpawnY = 15f;
    
    private SceneCalibrator _calibrator;
    private Tween _tween;

    private void OnEnable()
    {
        _calibrator = GetComponentInParent<SceneCalibrator>();

        _calibrator.OnCaliber += OnCaliber;
    }

    private void OnDisable()
    {
        _calibrator.OnCaliber -= OnCaliber;
    }

    private void OnCaliber()
    {
        var position = transform.position;
        position.y -= 0.5f;
        _tween = transform.DOMove(position, _calibrator.Duration).SetLink(gameObject);
        _tween.OnComplete(CorrectPosition);
    }

    private void CorrectPosition()
    {
        
        Vector2 position = transform.localPosition;
        Debug.Log(position.y <= EndY);
        if (position.y <= EndY)
        {
            transform.localPosition = new Vector2(position.x, SpawnY);
        }
    }
}

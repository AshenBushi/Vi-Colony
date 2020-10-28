using DG.Tweening;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private int _direction;
    
    private float _duration;
    private Tween _moveTween;

    private void Start()
    {
        _duration = 50 * transform.localScale.x + Random.Range(-5, 6);
        RotateObject();
    }

    private void RotateObject()
    {
        _moveTween = transform.DORotate(new Vector3(0f, 0f, 360f * _direction), _duration, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLink(gameObject);
        _moveTween.OnComplete(RotateObject);
    }
}

using DG.Tweening;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private int _direction;
    
    private Tween _moveTween;

    private void Start()
    {
        RotateObject();
    }

    private void RotateObject()
    {
        _moveTween = transform.DORotate(new Vector3(0f, 0f, 360f * _direction), _duration, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLink(gameObject);
        _moveTween.OnComplete(RotateObject);
    }
}

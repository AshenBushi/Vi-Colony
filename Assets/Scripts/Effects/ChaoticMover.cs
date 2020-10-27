using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChaoticMover : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _lapTime;
    [SerializeField] private Vector3 _startPoint;
    
    private Tween _moveTween;
    private Vector3 _pointToMove;

    private void Start()
    {
        _startPoint = transform.position;
        StartMove();
    }

    private void StartMove()
    {
        GeneratePointToMove();
        _moveTween = transform.DOMove(_pointToMove, Distance() * _lapTime).SetEase(Ease.Linear).SetLink(gameObject);
        _moveTween.OnComplete(StartMove);
    }

    private void GeneratePointToMove()
    {
        _pointToMove = new Vector3(_startPoint.x + Random.Range(-_radius, _radius), _startPoint.y + Random.Range(-_radius, _radius));
    }

    private float Distance()
    {
        var position = transform.position;
        return Mathf.Sqrt(Mathf.Pow(_pointToMove.x - position.x, 2) + Mathf.Pow(_pointToMove.y - position.y, 2));
    }
}

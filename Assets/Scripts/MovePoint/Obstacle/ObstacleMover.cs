using DG.Tweening;
using UnityEngine;

public class ObstacleMover : Obstacle
{
    private ObstacleSpawner _point;
    private Tween _tween;
    private Vector3 _pointPosition;
    private float _positionValue = 0;
    private float _angle;

    private void Start()
    {
        _point = GetComponentInParent<ObstacleSpawner>();
    }

    public void SetParameters(int radius, float speed, float startAngle, bool direction)
    {
        Radius = radius;
        Speed = speed;
        StartAngle = startAngle;
        Direction = direction;
        _angle = 0;
        _positionValue = 0;
    }

    private void Update()
    {
        MoveAround();
    }

    public void EnableObstacle()
    {
        gameObject.SetActive(true);
        transform.localScale = new Vector2(1f, 1f);
    }

    public void DisableObstacle()
    {
        _tween = transform.DOScale(new Vector2(0f, 0f), 0.4f).SetLink(gameObject);
        _tween.OnComplete(() => gameObject.SetActive(false));
    }

    private void MoveAround()
    {
        _pointPosition = _point.transform.position;
        transform.right = _pointPosition - transform.position;
        
        if (Direction)
        {
            if (_positionValue > 1)
                _positionValue = 0;
            else
                _positionValue += Time.deltaTime * Speed / 6.28f;
        }
        else
        {
            if (_positionValue < 0)
                _positionValue = 1;
            else
                _positionValue -= Time.deltaTime * Speed / 6.28f;
        }

        _angle = Mathf.Lerp(0 + StartAngle, 6.28f + StartAngle, _positionValue);
        
        var x = Mathf.Cos(_angle);
        var y = Mathf.Sin(_angle);
        
        transform.position = new Vector3(x * Radius, y * Radius, 0) + _pointPosition;
    }
}

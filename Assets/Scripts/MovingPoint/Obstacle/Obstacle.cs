using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _speed;
    [SerializeField] private float _angle;
    //[SerializeField] private int _damage;

    private MovingPoint _point;
    private Vector2 _pointPosition;

    private void Start()
    {
        _point = GetComponentInParent<MovingPoint>();
    }

    private void Update()
    {
        MoveAround();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.Die();
        }
    }

    private void MoveAround()
    {
        _pointPosition = _point.transform.position;
        _angle += Time.deltaTime;

        var cos = Mathf.Cos(_angle * _speed);
        var sin = Mathf.Sin(_angle * _speed);
        
        transform.position = new Vector2(cos * _radius, sin * _radius) + _pointPosition;
    }

    /*public void Destroy()
    {
        Destroy(gameObject);
    }*/
}

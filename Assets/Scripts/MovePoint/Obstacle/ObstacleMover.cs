﻿using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Obstacle))]
public class ObstacleMover : MonoBehaviour
{
    [SerializeField] private int _radius;
    [SerializeField] private float _speed;
    [SerializeField] private float _angle;
    [SerializeField] private float _startAngle;

    private ObstacleSpawner _point;
    private float _lapTime = 0;
    private Vector2 _pointPosition;

    private void Start()
    {
        _point = GetComponentInParent<ObstacleSpawner>();
    }

    public void SetParameters(int radius, float speed, float startAngle)
    {
        _radius = radius;
        _speed = speed;
        _startAngle = startAngle;
        _angle = 0;
        _lapTime = 0;
    }
    
    private void Update()
    {
        MoveAround();
    }

    private void MoveAround()
    {
        _pointPosition = _point.transform.position;
        if (_lapTime > 1)
            _lapTime = 0;
        else
            _lapTime += Time.deltaTime * _speed / 6.28f;

        _angle = Mathf.Lerp(0 + _startAngle, 6.28f + _startAngle, _lapTime);
        
        var x = Mathf.Cos(_angle);
        var y = Mathf.Sin(_angle);
        
        transform.position = new Vector2(x * _radius, y * _radius) + _pointPosition;
    }
}

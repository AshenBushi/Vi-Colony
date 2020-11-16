using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Pulse : MonoBehaviour
{
    private float _duration;
    private Vector3 _minScale;
    private Vector3 _maxScale;
    private Tween _tween;
    private Vector3 _startScale;
    private Vector3 _target;
    
    private void Start()
    {
        _startScale = transform.localScale;
        _minScale = _startScale * 0.8f;
        _maxScale = _startScale * 1.2f;
        _duration = 5 / _startScale.x  + Random.Range(-1, 2);
        
        ScalePulse();
    }

    private void ScalePulse()
    {
        _target = transform.localScale == _maxScale ? _minScale : _maxScale;
        
        _tween = transform.DOScale(_target, _duration).SetEase(Ease.Linear).SetLink(gameObject);
        _tween.OnComplete(ScalePulse);
    }
}

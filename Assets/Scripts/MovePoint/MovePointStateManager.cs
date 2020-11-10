using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MovePointStateManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _backSprite;
    [SerializeField] private GameObject _core;
    [SerializeField] private Sprite _infectedBack;
    [SerializeField] private Sprite _healthyBack;

    private Tween _tween;
    private float _animDuration = 0.4f;
    private bool _isInfected = false;
    
    public void Infect()
    {
        if (_isInfected) return;
        _isInfected = true;
        _tween = _core.transform.DOScale(Vector3.zero, _animDuration);
        _tween = _backSprite.transform.DOScale(Vector3.zero, _animDuration / 2);
        _tween.OnComplete(() =>
        {
            _backSprite.sprite = _infectedBack;
            _tween = _backSprite.transform.DOScale(Vector3.one, _animDuration / 2);
        });
    }

    public void Cure()
    {
        _isInfected = false;
        _core.transform.localScale = Vector3.one;
        _backSprite.sprite = _healthyBack;
        _backSprite.transform.localScale = Vector3.one;
    }
    
}

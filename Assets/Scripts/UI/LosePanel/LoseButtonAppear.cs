using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LoseButtonAppear : MonoBehaviour
{
    [SerializeField] private GameObject _image;
    [SerializeField] private StatsManager _statsManager;
    
    private void OnEnable()
    {
        _statsManager.OnAppear += DisableImage;
        _statsManager.OnAppeared += ShowButton;
    }

    private void OnDisable()
    {
        _statsManager.OnAppear -= DisableImage;
        _statsManager.OnAppeared -= ShowButton;
    }

    private void ShowButton()
    {
        _image.SetActive(true);
        transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f);
    }

    private void DisableImage()
    {
        _image.SetActive(false);
        transform.localScale = Vector3.zero;
    }
}

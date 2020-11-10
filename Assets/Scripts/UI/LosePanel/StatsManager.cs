using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class StatsManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _score;
    [SerializeField] private TMP_Text _playedTime;
    [SerializeField] private TMP_Text _dieCount;
    
    private Animator _animator;
    private Tween _tween;

    public event UnityAction OnAppear;
    public event UnityAction OnAppeared;
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void AppearStats(int score, int dieCount)
    {
        OnAppear?.Invoke();
        _animator.SetBool("IsStatsOpen", true);
        StartCoroutine(ShowStats(score, dieCount));
    }
    
    public void DisappearStats()
    {
        _animator.SetBool("IsStatsOpen", false);
    }

    private IEnumerator ShowStats(int score, int dieCount)
    {
        _score.text = 0.ToString();
        _dieCount.text = 0.ToString();
        
        yield return new WaitForSeconds(2f);

        OnAppeared?.Invoke();
        
        yield return new WaitForSeconds(0.5f);

        StartCoroutine(ShowDieCount(dieCount));
        
        for (var i = 0; i <= score; i++)
        {
            _score.text = i.ToString();
            yield return new WaitForSeconds(1.5f / score);
        }
    }

    private IEnumerator ShowDieCount(int dieCount)
    {
        for (var i = 0; i <= dieCount; i++)
        {
            _dieCount.text = i.ToString();
            yield return new WaitForSeconds(1.5f / dieCount);
        }
    }
    
    
}

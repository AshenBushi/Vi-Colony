using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePoint : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Animator _coreAnimator;
    [SerializeField] private Animator _backAnimator;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out Player player)) return;
        _coreAnimator.SetBool("Reached", true);
        _backAnimator.SetBool("Reached", true);
    }

    public void SpawnPoint()
    {
        _coreAnimator.SetBool("Reached", false);
        _backAnimator.SetBool("Reached", false);
    }

    public void ChangPointState(bool state)
    {
        _animator.SetBool("State", state);
    }
}

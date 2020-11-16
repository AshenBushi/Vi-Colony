using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerMover))]
public class Player : MonoBehaviour
{
    private const int _maxHealth = 100;
    
    private PlayerMover _mover;
    private int _health = _maxHealth;
    
    public int DieCount { get; private set; } = 0;

    public event UnityAction OnApplyDamage;
    public event UnityAction OnDied;

    private void Awake()
    {
        _mover = GetComponent<PlayerMover>();
    }

    public void ApplyDamage(int damage)
    {
        _health -= damage;
        OnApplyDamage?.Invoke();
        if(_health <= 0)
            Die();
    }

    private void Die()
    {
        _mover.DisableTap();
        DieCount += 1;
        OnDied?.Invoke();
    }
    
    public void Revive()
    {
        _health = _maxHealth;
        _mover.EnableTap();
    }
}

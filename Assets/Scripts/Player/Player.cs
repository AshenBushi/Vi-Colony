using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    private const float StartSpeed = 100;

    protected float _speed = StartSpeed;

    [SerializeField] protected int _health = 1000;

    [SerializeField] protected int _dieCount = 0;

    protected CircleCollider2D PlayerCollider;
    protected bool IsAlive = true;
    public int JumpsCount { get; protected set; } = 0;

    public event UnityAction<int, int> OnDied;
    public event UnityAction OnTakeDamage;

    protected void IncreaseSpeed()
    {
        if(_speed < 150)
            _speed = StartSpeed + JumpsCount / 2.5f;
    }

    public void TakeDamage(int damage)
    {
        PlayerCollider.enabled = false;
        _health -= damage;
        if(_health <= 0)
            IsDied();
        else
            OnTakeDamage?.Invoke();
    }

    private void IsDied()
    {
        if (_health > 0) return;
        _dieCount += 1;
        OnDied?.Invoke(JumpsCount, _dieCount);
    }
}

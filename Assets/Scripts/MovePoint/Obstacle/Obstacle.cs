using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private int _damage = 15;

    protected int Radius;
    protected float Speed;
    protected float StartAngle;
    protected bool Direction;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out Player player)) return;
        player.TakeDamage(_damage);
    }
}

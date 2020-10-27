using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerMover))]
public class Player : MonoBehaviour
{
    private PlayerMover _mover;
    private const int DefaultSpeed = 10;
    
    //public int Health { get; } = 100;
    public float Speed { get; private set;} = DefaultSpeed;
    public float PlayerJumps { get; private set; } = 0;
    
    public event UnityAction Jumping;

    private void OnEnable()
    {
        _mover = GetComponent<PlayerMover>();
        _mover.MakeJump += OnMakeJump;
    }

    private void OnDisable()
    {
        _mover.MakeJump -= OnMakeJump;
    }

    public void Die()
    {
        SceneManager.LoadScene(0);
    }

    private void OnMakeJump()
    {
        PlayerJumps++;
        Speed = DefaultSpeed + PlayerJumps / 100;
        Jumping?.Invoke();
    }
}

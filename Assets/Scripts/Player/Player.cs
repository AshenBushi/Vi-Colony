using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerMover))]
public class Player : MonoBehaviour
{
    private PlayerMover _mover;
    
    //public int Health { get; } = 100;
    public float Speed { get; } = 10;
    public float PlayerJumps { get; private set; } = 0;

    public event UnityAction Died;
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
        Died?.Invoke();
    }

    private void OnMakeJump()
    {
        PlayerJumps++;
        Jumping?.Invoke();
    }
}

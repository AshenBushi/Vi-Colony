using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerMover))]
public class Player : MonoBehaviour
{
    private PlayerMover _mover;
    
    //public int Health { get; } = 100;
    public float Speed { get; } = 100;
    public float PlayerJumps { get; private set; } = 0;

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
    }
}

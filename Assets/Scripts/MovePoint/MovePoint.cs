using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovePointStateManager))]
public class MovePoint : MonoBehaviour
{
    private MovePointStateManager _stateManager;
    private ObstacleSpawner _obstacleSpawner;
    
    public int Number { get; private set; }

    private void Awake()
    {
        _obstacleSpawner = GetComponentInChildren<ObstacleSpawner>();
        _stateManager = GetComponent<MovePointStateManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out Player player)) return;
        _obstacleSpawner.DisableAllObstacles(0);
        _stateManager.Infect();
    }
    
    public void Spawn(float x, float y, int number)
    {
        gameObject.SetActive(true);
        _stateManager.Cure();
        transform.position = new Vector2(x,y);
        Number = number;
        _obstacleSpawner.SpawnObstacles(Number);
    }
}

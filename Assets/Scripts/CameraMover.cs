using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraMover : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] private MovingPointsPool movingPointsPool;
    [SerializeField] private Transform[] movingPoints;

    public void CentredCamera()
    {
        
    }
}

using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private List<MovingPoint> _movingPoints = new List<MovingPoint>();
    }
}

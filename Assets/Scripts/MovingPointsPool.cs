using System.Collections.Generic;
using UnityEngine;

public class MovingPointsPool : MonoBehaviour
{
    [SerializeField] private List<GameObject> movingPoints = new List<GameObject>();
    private int _index = -1;

    public GameObject CurrentMovingPoint => movingPoints[_index];

    private void ScrollIndex(ref int index)
    {
        if (index < movingPoints.Count - 1)
            index++;
        else
            index = 0;
    }

    public GameObject GetNextMovingPoint()
    {
        ScrollIndex(ref _index);
        return movingPoints[_index];
    }

    public void DisableCurrentPoint()
    {
        movingPoints[_index].SetActive(false);
        SpawnNewMovingPoint();
    }

    private void SpawnNewMovingPoint()
    {
        var pointIndex = _index;
        
        ScrollIndex(ref pointIndex);
        
        var pointPosition = movingPoints[pointIndex].transform.position; 
        Vector2 coordinate = new Vector2(pointPosition.x + 2 * Random.Range(-2f, 2f), pointPosition.y + Random.Range(7f, 8f));
        
        ScrollIndex(ref pointIndex);
        movingPoints[pointIndex].transform.position = coordinate;
        movingPoints[pointIndex].SetActive(true);
    }
}

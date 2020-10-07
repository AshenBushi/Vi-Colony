using System.Collections.Generic;
using UnityEngine;

public class MovingPointsPool : MonoBehaviour
{
    [SerializeField] private List<GameObject> movingPoints = new List<GameObject>();
    private int _index = -1;

    public int Index => _index;

    public void NextIndex(ref int index)
    {
        if (index < movingPoints.Count - 1)
            index++;
        else
            index = 0;
    }

    public Transform GetMovingPoint(int index)
    {
        return movingPoints[index].transform;
    }
    
    public GameObject GetNextMovingPoint()
    {
        if(_index >= 0)
            movingPoints[_index].SetActive(false);
        NextIndex(ref _index);
        return movingPoints[_index];
    }

    public void SpawnNewMovingPoint()
    {
        var pointIndex = _index;
        
        NextIndex(ref pointIndex);
        
        var pointPosition = movingPoints[pointIndex].transform.position;

        NextIndex(ref pointIndex);
        movingPoints[pointIndex].transform.position = new Vector2(2 + 2 * Random.Range(-2.5f, 2.5f), pointPosition.y + Random.Range(5f, 8f));
        movingPoints[pointIndex].SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaterpillarEnemy : MonoBehaviour
{
    List<CaterpillarSegment> Segments = new();
    [SerializeField]
    CaterpillarSegment SegmentPrefab;
    [SerializeField]
    float distanceBetweenSegments = 1;
    [SerializeField]
    float movementSpeed = 1;
    public Lane parentLane;
    public void Spawn(int segments)
    {
        for (int i = 0; i < segments; i++)
        {
            CaterpillarSegment segment = Instantiate(SegmentPrefab, transform);
            segment.parentLane = parentLane;
        }
    }
    void Update()
    {
        int i = 0;
        foreach (CaterpillarSegment segment in Segments)
        {
            segment.transform.position = transform.position + Vector3.right * distanceBetweenSegments * i;
            i++;
        }
    }
}

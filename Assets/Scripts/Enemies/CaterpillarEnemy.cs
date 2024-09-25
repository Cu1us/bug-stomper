using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

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
        bool flip = false;
        for (int i = 0; i < segments; i++)
        {
            CaterpillarSegment segment = Instantiate(SegmentPrefab, transform);
            segment.parentLane = parentLane;
            segment.onDeath += OnSegmentDeath;
            segment.SetFlipped(flip);
            if (Random.Range(0, 3) != 0) // 2/3 chance
                flip = !flip;
            segment.transform.localPosition = Vector3.right * i * distanceBetweenSegments;
        }
    }
    void OnSegmentDeath(CaterpillarSegment segment)
    {
        Segments.Remove(segment);
        if (Segments.Count == 0)
        {
            Destroy(this);
        }
    }
    bool IsEverySegmentFlipped()
    {
        foreach (CaterpillarSegment segment in Segments)
        {
            if (!segment.flipped)
                return false;
        }
        return true;
    }
    void Update()
    {
        if (!IsEverySegmentFlipped())
        {
            // Move parent
            float moveDistance = movementSpeed * Time.fixedDeltaTime;
            if (parentLane && parentLane.currentWave != null)
            {
                moveDistance *= parentLane.currentWave.enemyMoveSpeedMultiplier;
            }
            transform.position += Vector3.left * moveDistance;
        }
    }
}

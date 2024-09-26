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
    [SerializeField]
    int pointsOnKill = 100;
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
            segment.transform.localPosition = Vector3.right * i * distanceBetweenSegments;
            if (Random.Range(0, 3) != 0) // 2/3 chance
                flip = !flip;
            Segments.Add(segment);
        }
    }
    void OnSegmentDeath(CaterpillarSegment segment)
    {
        Segments.Remove(segment);
        if (Segments.Count == 0)
        {
            Destroy(gameObject);
        }
        if (Segments.Count == 1)
        {
            Segments[0].pointsOnKill = pointsOnKill;
        }
        transform.position += Vector3.right * distanceBetweenSegments;
        int i = 0;
        foreach (CaterpillarSegment segment1 in Segments)
        {
            segment1.transform.localPosition = Vector3.right * i * distanceBetweenSegments;
            i++;
        }
    }
    bool IsAnySegmentWalking()
    {
        foreach (CaterpillarSegment segment in Segments)
        {
            if (!segment.flipped)
                return true;
        }
        return false;
    }
    void Update()
    {
        if (IsAnySegmentWalking())
        {
            // Move parent
            float moveDistance = movementSpeed * Time.fixedDeltaTime;
            if (parentLane && parentLane.currentWave != null)
            {
                moveDistance *= parentLane.currentWave.enemyMoveSpeedMultiplier;
            }
            transform.position += Vector3.left * moveDistance;
        }
        if (transform.position.x < GameManager.instance.LaneEndTransform.position.x) // If at the end of the lane
        {
            OnReachEnd();
        }
    }
    public void OnReachEnd()
    {
        GameManager.instance.OnEnemyReachEnd(Segments[0]);
    }
}

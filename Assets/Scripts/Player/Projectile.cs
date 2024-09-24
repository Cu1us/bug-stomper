using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Projectile : MonoBehaviour
{
    const float gravityWhenDeflected = 3.5f;
    [SerializeField][Range(0.1f, 50f)] float baseSpeed = 2;
    [SerializeField][Range(-200f, 50f)] float rotation = 2;
    [SerializeField][Range(1, 10)] int damage = 1;
    SpriteRenderer sr;

    Vector2 velocity;
    bool hasBeenDeflected = false;

    void Start()
    {
        velocity = Vector2.right * baseSpeed;
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (hasBeenDeflected)
        {
            velocity -= Vector2.up * Time.deltaTime * gravityWhenDeflected;
        }
        transform.position += (Vector3)velocity * Time.deltaTime;
        transform.Rotate(0, 0, rotation * Time.deltaTime);
        if (!sr.isVisible) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasBeenDeflected) return;
        Enemy hit = other.GetComponent<Enemy>();
        if (hit != null)
        {
            ReturnBehavior returnBehavior = hit.HitByProjectile(damage);
            switch (returnBehavior)
            {
                case ReturnBehavior.HIT:
                    Destroy(gameObject);
                    break;
                case ReturnBehavior.DEFLECT:
                    hasBeenDeflected = true;
                    velocity = -velocity + Vector2.up * Random.Range(-1f, 1.75f);
                    break;
            }
        }
    }

    public enum ReturnBehavior
    {
        HIT,
        PASSTHROUGH,
        DEFLECT
    }
}

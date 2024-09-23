using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField][Range(0.1f,10)] float velocity = 2;
    [SerializeField][Range(1,10)] int health = 2;
    [SerializeField] bool flipped;
    SpriteRenderer sr;
    public Action onDie;
    //[SerializeField] Lane currentLane;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!flipped) transform.position += new Vector3(-(velocity*Time.deltaTime),0);
        
    }

    public void Flip()
    {
        flipped = !flipped;
        sr.flipY = ! sr.flipY;

    }

    public void Damage(int damageIn)
    {
        if (flipped)
        {
            health -= damageIn;
        }

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        onDie?.Invoke();
        Destroy(gameObject);
    }
}

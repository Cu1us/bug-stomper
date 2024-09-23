using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField][Range(0.1f,10)] float velocity = 2;
    [SerializeField][Range(-200f,50f)] float rotation = 2;
    [SerializeField][Range(1,10)] int damage = 1;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position += new Vector3(velocity*Time.deltaTime,0);
        transform.Rotate(0,0,rotation*Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        Enemy hit = other.GetComponent<Enemy>();
        if (hit != null)
        {
            hit.Damage(damage);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomp : MonoBehaviour
{
    [SerializeField] float velocity = 0.5f;
    BoxCollider2D collider2d;
    

    void Start()
    {
        collider2d = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        transform.position += new Vector3(velocity*Time.deltaTime,0);
        if (transform.position.x >= 6) collider2d.enabled = false;

        if (transform.position.x >= 10) Destroy(gameObject);

    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        Enemy hit = other.GetComponent<Enemy>();
        if (hit != null)
        {
            hit.HitByShockwave();
        }
    }
}

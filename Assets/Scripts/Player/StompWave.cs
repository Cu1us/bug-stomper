using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompWave : MonoBehaviour
{
    [SerializeField] float velocity = 0.5f;
    //SpriteRenderer sr;
    MeshRenderer test;

    void Start()
    {
        //sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        transform.position += new Vector3(velocity*Time.deltaTime,0);

        //if (!sr.isVisible) Destroy(gameObject);
        if (transform.position.x >= 15) Destroy(gameObject);

    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        Enemy hit = other.GetComponent<Enemy>();
        if (hit != null)
        {
            hit.Flip();
        }
    }
}

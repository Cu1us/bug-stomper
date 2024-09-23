using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompWave : MonoBehaviour
{
    [SerializeField] float velocity = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(velocity*Time.deltaTime,0);
    }
}

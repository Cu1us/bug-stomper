using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int currentLane = 0;
    [SerializeField] float laneYSize = 5;
    [SerializeField] float fireRate = .5f;
     float fireTimer;
    [SerializeField] float stompRate = 5f;
     float stompTimer;
    [SerializeField] Projectile projectile;
    [SerializeField] StompWave stompWave;

    void Start()
    {
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        if (Input.GetButtonDown("Vertical"))
        {
            currentLane = Mathf.Clamp(currentLane + (int)Input.GetAxisRaw("Vertical"), 0, 2);
            //Debug.Log(currentLane);
            transform.position = new Vector3(transform.position.x, (currentLane-1) * laneYSize);
        }
        stompTimer -= Time.deltaTime;
        fireTimer -= Time.deltaTime;

        if (Input.GetButtonDown("Jump") && stompTimer <= 0)
        {
            Vector3 spawnPos = transform.position;
            Instantiate(stompWave, spawnPos, Quaternion.identity);
            stompTimer = stompRate;
            Debug.Log("STOMP!");
        }

        if (Input.GetButtonDown("Fire1") && fireTimer <= 0)
        {
            Vector3 spawnPos = transform.position;
            Instantiate(projectile, spawnPos, Quaternion.identity);
            fireTimer = fireRate;
            Debug.Log("Fire!!");
        }
    }
}

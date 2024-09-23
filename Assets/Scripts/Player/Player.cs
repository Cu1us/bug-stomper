using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int laneNumber = 0;
    Lane currentLane;
    [SerializeField] float laneYSize = 5;
    [SerializeField] float fireRate = .5f;
    float fireTimer;
    [SerializeField] float stompRate = 5f;
    float stompTimer;
    [SerializeField] Projectile projectile;
    [SerializeField] StompWave stompWave;
    [SerializeField] GameManager gameManager;
    [SerializeField] Transform throwTransform;

    void Start()
    {
        Application.targetFrameRate = 60;
        float y = gameManager.Lanes[laneNumber].gameObject.transform.position.y;
        transform.position = new Vector3(transform.position.x, y);
    }

    void Update()
    {
        if (Input.GetButtonDown("Vertical"))
        {
            laneNumber = Mathf.Clamp(laneNumber + (int)Input.GetAxisRaw("Vertical"), 0, 2);
            float y = gameManager.Lanes[laneNumber].gameObject.transform.position.y;
            transform.position = new Vector3(transform.position.x, y);
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
            Vector3 spawnPos = throwTransform.position;
            Instantiate(projectile, spawnPos, Quaternion.identity);
            fireTimer = fireRate;
            Debug.Log("Fire!!");
        }
    }
}

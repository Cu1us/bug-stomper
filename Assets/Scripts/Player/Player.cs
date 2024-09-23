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
    [SerializeField] Transform stompTransform;
    Animator animator;
    [SerializeField] float offY = -0.3f;

    void Start()
    {
        animator = GetComponent<Animator>();
        Application.targetFrameRate = 60;
        SetLane();
    }

    void Update()
    {
        if (Input.GetButtonDown("Vertical"))
        {
            SetLane();
        }
        stompTimer -= Time.deltaTime;
        fireTimer -= Time.deltaTime;

        if (Input.GetButtonDown("Jump") && stompTimer <= 0)
        {
            Vector3 spawnPos = stompTransform.position;
            Instantiate(stompWave, spawnPos, Quaternion.identity);
            stompTimer = stompRate;
        }

        if (Input.GetButtonDown("Fire1") && fireTimer <= 0)
        {
            Vector3 spawnPos = throwTransform.position;
            Instantiate(projectile, spawnPos, Quaternion.identity);
            fireTimer = fireRate;
            // animator.SetTrigger("throw");
            animator.Play("GnomeThrow");
        }
    }

    private void SetLane()
    {
        laneNumber = Mathf.Clamp(laneNumber + (int)Input.GetAxisRaw("Vertical"), 0, 2);
        float y = gameManager.Lanes[laneNumber].gameObject.transform.position.y;
        transform.position = new Vector3(transform.position.x, y+offY);
    }
}

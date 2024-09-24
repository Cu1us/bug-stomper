using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]

public class Player : MonoBehaviour
{
    public int laneNumber = 0;
    Lane currentLane;
    [Header("Parameters")]
    [SerializeField] float fireRate = .5f;
    [SerializeField] float stompRate = 5f;
    [SerializeField] float tweakYPos = -0.3f;

    [Header("References")]
    [SerializeField] Transform throwTransform;
    [SerializeField] Transform stompTransform;
    [SerializeField] GameManager gameManager;
    [SerializeField] Projectile projectile;
    [SerializeField] Stomp stompWave;
    Animator animator;

    //local vars
    float fireTimer;
    float stompTimer;

    void Start()
    {
        animator = GetComponent<Animator>();
        SetLane();
    }

    // TODO
    // Add states to prevent movement and actions when in other states than idle.

    void Update()
    {
        if (Input.GetButtonDown("Fire2")) gameManager.StartGame();

        if (Input.GetButtonDown("Vertical"))
        {
            SetLane();
        }
        stompTimer -= Time.deltaTime;
        fireTimer -= Time.deltaTime;

        if (Input.GetButtonDown("Jump") && stompTimer <= 0)
        {
            stompTimer = stompRate;
            animator.Play("GnomeStomp");
            Invoke(nameof(Stomp), stompRate);
        }

        if (Input.GetButtonDown("Fire1") && fireTimer <= 0)
        {
            fireTimer = fireRate;
            Vector3 spawnPos = throwTransform.position;
            Instantiate(projectile, spawnPos, Quaternion.identity);
            animator.Play("GnomeThrow");
        }
    }

    void Stomp()
    {
        Vector3 spawnPos = stompTransform.position;
        Instantiate(stompWave, spawnPos, Quaternion.identity);
    }

    private void SetLane()
    {
        laneNumber = Mathf.Clamp(laneNumber + (int)Input.GetAxisRaw("Vertical"), 0, 2);
        float y = gameManager.Lanes[laneNumber].gameObject.transform.position.y;
        transform.position = new Vector3(transform.position.x, y + tweakYPos);
    }
}

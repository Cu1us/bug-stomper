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
    float stompChargeTime;

    void Start()
    {
        animator = GetComponent<Animator>();
        SetLane();
    }

    // TODO
    // Add states to prevent movement and actions when in other states than idle.

    void Update()
    {
        stompTimer -= Time.deltaTime;
        fireTimer -= Time.deltaTime;

        if (Input.GetButtonDown("Fire2")) gameManager.StartGame();

        if (Input.GetButtonDown("Vertical") && stompChargeTime <= 0) SetLane();

        Stomp();

        if (Input.GetButtonDown("Fire1") && fireTimer <= 0  && stompChargeTime <= 0)
        {
            fireTimer = fireRate;
            Vector3 spawnPos = throwTransform.position;
            Instantiate(projectile, spawnPos, Quaternion.identity);
            animator.Play("GnomeThrow");
        }
    }

    void Stomp()
    {
        if (Input.GetButtonDown("Jump") && stompTimer <= 0)
        {
            animator.Play("GnomeCharge");
        }

        if (Input.GetButton("Jump"))
        {
            stompChargeTime += Time.deltaTime;
        }

        if (Input.GetButtonUp("Jump"))
        {
            if (stompChargeTime < 0.1f)
            {
                stompChargeTime = 0;
                animator.Play("GnomeIdle");
                return;
            }
            CameraShake.Play(Mathf.Min(stompChargeTime / 2, 1.5f));
            animator.Play("GnomeStomp");
            stompTimer = stompRate;
            StartStomp(laneNumber);
            if (stompChargeTime >= 1.0f)
            {
                if (laneNumber == 1)
                {
                    StartStomp(laneNumber+1);
                    StartStomp(laneNumber-1);
                }
                else
                {
                    if (laneNumber == 2)
                    {
                        StartStomp(laneNumber-1);
                    }
                    
                    if (laneNumber == 0)
                    {
                        StartStomp(laneNumber+1);
                    }
                }
            }
            stompChargeTime = 0;
        }
    }

    void StartStomp(int laneIn)
    {
        Vector3 spawnPos = stompTransform.position;
        spawnPos.y = gameManager.Lanes[laneIn].gameObject.transform.position.y + tweakYPos;
        Instantiate(stompWave, spawnPos, Quaternion.identity);
        animator.Play("GnomeStomp");
    }

    private void SetLane()
    {
        laneNumber = Mathf.Clamp(laneNumber + (int)Input.GetAxisRaw("Vertical"), 0, 2);
        float y = gameManager.Lanes[laneNumber].gameObject.transform.position.y;
        transform.position = new Vector3(transform.position.x, y + tweakYPos);
    }
}

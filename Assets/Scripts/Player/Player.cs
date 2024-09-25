using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

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
    [SerializeField] float minChargeTime = .15f;
    [SerializeField] float maxChargeTime = .5f;

    [Header("References")]
    [SerializeField] Transform throwTransform;
    [SerializeField] Transform stompTransform;
    [SerializeField] GameManager gameManager;
    [SerializeField] Projectile projectile;
    [SerializeField] Stomp stompWave;
    Animator animator;

    public UnityEvent<float> onStomp;
    public UnityEvent onSmallStomp;
    public UnityEvent onBigStomp;
    public UnityEvent onStompChargeStart;
    public UnityEvent onStompChargeEnd;

    //local vars
    float fireTimer;
    float stompTimer;
    float stompChargeTimer;

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

        if (Input.GetButtonDown("Vertical") && stompChargeTimer <= 0) SetLane();

        Stomp();


        if (Input.GetButtonDown("Fire1") && fireTimer <= 0 && stompTimer <= 0 && stompChargeTimer <= 0)
        {
            fireTimer = fireRate;
            Vector3 spawnPos = throwTransform.position;
            Instantiate(projectile, spawnPos, Quaternion.identity);
            animator.Play("GnomeThrow");
            AudioController.Play("ThrowMushroom");
        }
    }

    void Stomp()
    {
        if (stompTimer > 0 && stompChargeTimer <= 0) return;

        if (Input.GetButtonDown("Jump"))
        {
            stompChargeTimer = 0;
            animator.SetBool("charge", true);
            animator.SetBool("stomp", false);
            onStompChargeStart?.Invoke();
        }

        if (Input.GetButton("Jump"))
        {
            stompChargeTimer += Time.deltaTime;
        }

        if (Input.GetButtonUp("Jump"))
        {
            onStompChargeEnd?.Invoke();
            if (stompChargeTimer < minChargeTime)
            {
                stompChargeTimer = 0;
                stompTimer = stompRate;
                animator.SetBool("charge", false);
                animator.SetBool("stomp", false);
                animator.Play("GnomeIdle");
                return;
            }

            CameraShake.Play(Mathf.Clamp(stompChargeTimer / 2, 0.15f, 1.25f));
            animator.SetBool("stomp", true);
            animator.SetBool("charge", false);
            StartStomp(laneNumber);

            onStomp?.Invoke(Mathf.Min(stompChargeTimer, maxChargeTime));
            if (stompChargeTimer >= maxChargeTime)
            {
                onBigStomp?.Invoke();
                AudioController.Play("BigStomp");
                if (laneNumber == 1)
                {
                    StartStomp(laneNumber + 1);
                    StartStomp(laneNumber - 1);
                }
                else
                {
                    if (laneNumber == 2)
                    {
                        StartStomp(laneNumber - 1);
                    }

                    if (laneNumber == 0)
                    {
                        StartStomp(laneNumber + 1);
                    }
                }
            }
            else
            {
                onSmallStomp?.Invoke();
                AudioController.Play("SmallStomp");
            }
            stompChargeTimer = 0;
            stompTimer = stompRate;
        }
    }

    void StartStomp(int laneIn)
    {
        Vector3 spawnPos = stompTransform.position;
        spawnPos.y = gameManager.Lanes[laneIn].gameObject.transform.position.y + tweakYPos;
        Instantiate(stompWave, spawnPos, Quaternion.identity);
    }

    private void SetLane()
    {
        laneNumber = Mathf.Clamp(laneNumber + (int)Input.GetAxisRaw("Vertical"), 0, 2);
        float y = gameManager.Lanes[laneNumber].gameObject.transform.position.y;
        transform.position = new Vector3(transform.position.x, y + tweakYPos);
    }
}

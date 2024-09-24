using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class Enemy : MonoBehaviour
{
    public const float hurtBlinkDuration = 0.25f;
    [SerializeField] protected float movementSpeed = 2;
    [SerializeField] protected int health = 2;
    [SerializeField] protected bool flipped;
    protected SpriteRenderer sr;
    protected Animator animator;
    protected Rigidbody2D rb;
    public Lane parentLane;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public abstract Projectile.ReturnBehavior HitByProjectile(int damage);
    public abstract void HitByShockwave();

    public virtual void Flip()
    {
        sr.flipY = flipped = !flipped;
    }
    void FixedUpdate()
    {
        Move();
    }

    protected virtual void Move()
    {
        float moveDistance = movementSpeed * Time.fixedDeltaTime;
        if (parentLane && parentLane.currentWave != null)
        {
            moveDistance *= parentLane.currentWave.enemyMoveSpeedMultiplier;
        }
        rb.position += Vector2.left * moveDistance;
        if (rb.position.x < GameManager.instance.LaneEndTransform.position.x) // If at the end of the lane
        {
            OnReachEnd();
        }
    }

    public virtual void Damage(int damage)
    {
        health -= damage;
        if (health > 0)
        {
            BlinkHurt();
        }
        else
        {
            Kill();
        }
    }
    public virtual void BlinkHurt()
    {
        sr.color = Color.red;
        CancelInvoke(nameof(UnblinkHurt));
        Invoke(nameof(UnblinkHurt), hurtBlinkDuration);
    }
    public virtual void UnblinkHurt()
    {
        sr.color = Color.white;
    }
    public void Kill()
    {
        Destroy(gameObject);
    }
    public void OnReachEnd()
    {
        GameManager.instance.OnEnemyReachEnd(this);
    }
}

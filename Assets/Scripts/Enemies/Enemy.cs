using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public abstract class Enemy : MonoBehaviour
{
    public const float hurtBlinkDuration = 0.25f;
    [SerializeField] protected float velocity = 2;
    [SerializeField] protected int health = 2;
    [SerializeField] protected bool flipped;
    protected SpriteRenderer sr;
    protected Animator animator;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public abstract Projectile.ReturnBehavior HitByProjectile(int damage);
    public abstract void HitByShockwave();

    public virtual void Flip()
    {
        sr.flipY = flipped = !flipped;
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
}

using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CaterpillarSegment : Enemy
{
    bool isFlipping = false;
    public System.Action<CaterpillarSegment> onDeath;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public override Projectile.ReturnBehavior HitByProjectile(int damage)
    {
        if (isFlipping)
            return Projectile.ReturnBehavior.PASSTHROUGH;
        if (flipped)
        {
            Damage(damage);
            return Projectile.ReturnBehavior.HIT;
        }
        return Projectile.ReturnBehavior.DEFLECT;
    }
    public void OnFinishFlip()
    {
        isFlipping = false;
        SetWalkAnimation();
    }
    public override void HitByShockwave()
    {
        Flip();
    }
    public override void Flip()
    {
        base.Flip();
        if (flipped)
            animator.Play("FlipToUp");
        else
            animator.Play("FlipToDown");
    }
    public void SetFlipped(bool isFlipped)
    {
        flipped = isFlipped;
        SetWalkAnimation();
    }
    void SetWalkAnimation()
    {
        if (flipped)
            animator.Play("WalkUp");
        else
            animator.Play("WalkDown");
    }
    public override void Kill()
    {
        onDeath?.Invoke(this);
        base.Kill();
    }
    public override void Move()
    {
        return;
    }
}

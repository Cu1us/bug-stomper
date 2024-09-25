using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CaterpillarSegment : Enemy
{
    public System.Action<CaterpillarSegment> onDeath;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public override Projectile.ReturnBehavior HitByProjectile(int damage)
    {
        if (flipped)
        {
            Damage(damage);
            return Projectile.ReturnBehavior.HIT;
        }
        return Projectile.ReturnBehavior.DEFLECT;
    }

    public override void HitByShockwave()
    {
        Flip();
        UpdateAnimation();
    }
    public void SetFlipped(bool isFlipped)
    {
        flipped = isFlipped;
        UpdateAnimation();
    }
    void UpdateAnimation()
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
}

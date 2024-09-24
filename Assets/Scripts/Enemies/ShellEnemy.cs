using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellEnemy : Enemy
{
    [SerializeField]
    float flipAnimationTime;
    [SerializeField]
    AnimationCurve flipAnimationCurve;
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
    }

    protected override void Move()
    {
        if (!flipped && (Time.time - lastFlipTime) > flipAnimationTime / 2) base.Move();
    }
    void Update()
    {
        float flipProgress = Mathf.Clamp((Time.time - lastFlipTime) / flipAnimationTime, 0f, 1f);
        flipProgress = flipAnimationCurve.Evaluate(flipProgress);
        if (flipped)
        {
            transform.eulerAngles = new Vector3(0, 0, -180 * flipProgress);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 180 - 180 * flipProgress);
        }
    }
}

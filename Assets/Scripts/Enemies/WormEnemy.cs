using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormEnemy : Enemy
{
    [SerializeField]
    float flipAnimationTime;
    [SerializeField]
    AnimationCurve flipAnimationCurve;

    float flippedTime = 0;
    [SerializeField]
    float timeTakenToUnflip = 2f;
    public override Projectile.ReturnBehavior HitByProjectile(int damage)
    {
        Damage(damage);
        return Projectile.ReturnBehavior.HIT;
    }

    public override void HitByShockwave()
    {
        Flip();
        flippedTime = 0;
    }
    void Update()
    {
        flippedTime += Time.deltaTime;
        if (flipped && flippedTime > timeTakenToUnflip)
        {
            Flip();
        }

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

    public override void Move()
    {
        if (!flipped && (Time.time - lastFlipTime) > flipAnimationTime / 2) base.Move();
    }
    public override void Flip()
    {
        animator.Play("Flip");
        base.Flip();
    }
}

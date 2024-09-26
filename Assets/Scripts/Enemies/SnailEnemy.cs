using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SnailEnemy : Enemy
{
    [SerializeField]
    float flipAnimationTime;
    [SerializeField]
    AnimationCurve flipAnimationCurve;
    [SerializeField]
    float flipJumpHeight;

    float flipGroundY;
    bool inShell = false;
    public override Projectile.ReturnBehavior HitByProjectile(int damage)
    {
        if (!inShell)
        {
            health -= damage;
            BlinkHurt();
            if (health <= 0)
            {
                health = 0;
                inShell = true;
                GetComponent<Animator>().Play("EnterShell");
            }
            return Projectile.ReturnBehavior.HIT;
        }
        return Projectile.ReturnBehavior.DEFLECT;
    }


    public override void HitByShockwave()
    {
        if (inShell && !flipped)
        {
            Flip();
            flipGroundY = transform.position.y;
        }
    }
    public override void Move()
    {
        if (!inShell && !flipped) base.Move();
    }
    void Update()
    {
        if (flipped && inShell)
        {
            float flipProgress = Mathf.Clamp((Time.time - lastFlipTime) / flipAnimationTime, 0f, 1f);
            float flipAnimProgress = flipAnimationCurve.Evaluate(flipProgress);
            transform.eulerAngles = new Vector3(0, 0, -180 * flipAnimProgress);

            float jumpY = -Mathf.Pow(flipProgress * 2 - 1, 2) + 0.9f;
            transform.position = new Vector3(transform.position.x, flipGroundY + jumpY, transform.position.z);

            if (flipProgress >= 1)
            {
                flipped = false;
                DeathSprite.SpawnDeathSprite(transform.position, DeathSprite.EnemyType.SNAIL);
                AudioController.Play("Kill");
                base.Kill();
            }
        }
    }
}

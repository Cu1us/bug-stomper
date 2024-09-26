using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormEnemy : Enemy
{

    public override Projectile.ReturnBehavior HitByProjectile(int damage)
    {
        Damage(damage);
        return Projectile.ReturnBehavior.HIT;
    }

    public override void HitByShockwave()
    {
        Flip();
    }

    public override void Move()
    {
        if (!flipped)
            base.Move();
    }
    public override void Flip()
    {
        if (flipped) return;
        flipped = true;
        animator.Play("Flip");
    }

    public void OnFlipFinish()
    {
        flipped = false;
        animator.Play("Walk");
    }

    public override void Kill()
    {
        BlinkHurt();
        DeathSprite.SpawnDeathSprite(transform.position, DeathSprite.EnemyType.WORM);
        base.Kill();
    }
}

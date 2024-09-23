using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellEnemy : Enemy
{
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
}

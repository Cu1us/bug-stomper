using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormEnemy : Enemy
{
    public override Projectile.ReturnBehavior HitByProjectile(int damage)
    {
        return Projectile.ReturnBehavior.HIT;
    }

    public override void HitByShockwave()
    {
        
    }
}

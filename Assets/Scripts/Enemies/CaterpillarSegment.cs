using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaterpillarSegment : Enemy
{
    public System.Action<CaterpillarSegment> onDeath;
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
        sr.flipY = flipped;
    }
    public void SetFlipped(bool isFlipped)
    {
        flipped = isFlipped;
        sr.flipY = flipped;
    }
    public override void Kill()
    {
        onDeath?.Invoke(this);
        base.Kill();
    }
}

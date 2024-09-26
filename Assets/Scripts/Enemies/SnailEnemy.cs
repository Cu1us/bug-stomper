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
    [SerializeField]
    float timeToRespawnFromShell = 5f;

    float enterShellTime = 0;

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
                enterShellTime = Time.time;
                GetComponent<Animator>().Play("EnterShell");
            }
            return Projectile.ReturnBehavior.HIT;
        }
        return Projectile.ReturnBehavior.DEFLECT;
    }


    public override void HitByShockwave()
    {
        if (!inShell && !flipped)
        {
            animator.Play("Shockwave");
        }
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
        else if (!flipped && inShell)
        {
            if (Time.time - enterShellTime > timeToRespawnFromShell)
            {
                animator.Play("ExitShell");
                inShell = false;
                health = 3;
            }
        }
    }

    public void OnHurtFinish()
    {
        if (!inShell && !flipped) animator.Play("Walk");
    }
    public void OnExitShellFinish()
    {
        if (!inShell && !flipped) animator.Play("Walk");
    }
}

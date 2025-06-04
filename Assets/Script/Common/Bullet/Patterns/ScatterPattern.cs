using System.Collections;
using Script.Feature.Bullet;
using UnityEngine;

[CreateAssetMenu(fileName = "New Scatter Pattern", menuName = "Bullet Pattern/Scatter")]
public class ScatterPattern : BulletPattern{
    public float pelletMultiplier = 0.25f;
    public int pelletCount = 5;
    public override IEnumerator Init(BulletPool bulletPool, Vector2 direction, Transform shooter, int damage, EntityType targetType, IBulletBehavior behavior = null) {
        bulletPool.BulletRequest(shooter.position, direction)
                  .WithDamage(Mathf.FloorToInt(damage * damageMultiplier))
                  .WithLifetime(bulletLifetime)
                  .WithSpeed(bulletSpeed)
                  .WithTargetType(targetType)
                  .WithBehaviour(new RecursiveBehavior(bulletPool, Mathf.FloorToInt(pelletMultiplier * damage), pelletCount));

        yield return new WaitForSeconds(cooldown);
    }
}

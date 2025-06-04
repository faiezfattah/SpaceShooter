using System.Collections;
using Script.Feature.Bullet;
using UnityEngine;


[CreateAssetMenu(fileName = "New Single Pattern", menuName = "Bullet Pattern/Single")]
public class SinglePattern : BulletPattern {
    public override IEnumerator Init(BulletPool bulletPool, Vector2 direction, Transform shooter, int damage, EntityType targetType, IBulletBehavior behavior = null) {
        bulletPool.BulletRequest(shooter.position, direction)
                  .WithDamage(Mathf.FloorToInt(damage * damageMultiplier))
                  .WithLifetime(bulletLifetime)
                  .WithSpeed(bulletSpeed)
                  .WithTargetType(targetType);
        yield return new WaitForSeconds(cooldown);
    }
}

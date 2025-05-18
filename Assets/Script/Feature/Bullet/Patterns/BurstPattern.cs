using System.Collections;
using Script.Feature.Bullet;
using UnityEngine;
[CreateAssetMenu(fileName = "Burst Pattern", menuName = "Bullet Pattern/Burst")]
public class BurstPattern : BulletPattern {
    [SerializeField] int bulletPerBurstCount = 5;
    [SerializeField] float inbetweenCooldown = 0.2f;
    public override IEnumerator Init(
        BulletPool bulletPool,
        Vector2 dir,
        Transform shooter,
        int damage,
        EntityType type,
        IBulletBehavior behavior = null) {
        
        for (int i = 0; i < bulletPerBurstCount; i++) {
            bulletPool.BulletRequest(shooter.position, dir)
                    .WithDamage(damage)
                    .WithTargetType(type)
                    .WithLifetime(bulletLifetime)
                    .WithSpeed(bulletSpeed);

            yield return new WaitForSeconds(inbetweenCooldown);
        }
    }
}
using System.Collections;
using Script.Feature.Bullet;
using UnityEngine;

[CreateAssetMenu(fileName = "Spread Pattern", menuName = "Bullet Pattern/Spread")]
public class BulletSpreadPattern : BulletPattern {
    [SerializeField] int bulletCount = 3;
    [SerializeField] float spreadAngle = 15f;
    public override IEnumerator Init(
        BulletPool bulletPool,
        Vector2 direction,
        Transform shooter,
        int damage,
        EntityType targetType,
        IBulletBehavior behavior = null) {
            
        for (int i = -Mathf.FloorToInt(bulletCount / 2); i <= Mathf.FloorToInt(bulletCount/2); i++) {
            var dir = Quaternion.AngleAxis(i * spreadAngle, Vector3.forward) * direction;
            bulletPool.BulletRequest(shooter.position, dir)
                      .WithDamage(damage)
                      .WithTargetType(targetType)
                      .WithLifetime(bulletLifetime)
                      .WithSpeed(bulletSpeed);
        }

        yield return new WaitForSeconds(cooldown);
    }
}
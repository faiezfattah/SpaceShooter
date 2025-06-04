using UnityEngine;

namespace Script.Feature.Bullet {
public class RecursiveBehavior : IBulletBehavior {
    readonly BulletPool _pool;
    readonly int _bulletCount;
    readonly int _damage;
    public RecursiveBehavior(BulletPool pool, int damage, int bulletCount = 5) {
        _pool = pool;
        _bulletCount = bulletCount;
        _damage = damage;
    }

    public void OnImpact(Bullet bullet, Collision2D collision) {
        var contactPoint = collision.GetContact(0).point;

        float angleStep = 360f / _bulletCount;

        for (int i = 0; i < _bulletCount; i++) {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

            _pool.BulletRequest(contactPoint, direction)
                 .WithDamage(_damage)
                 .WithLifetime(bullet.Lifetime / 2)
                 .WithSpeed(bullet.Speed)
                 .WithTargetType(bullet.Type);

            Debug.Log($"Spawned scatter bullet #{i + 1} in direction {direction}");
        }
    }
}
}

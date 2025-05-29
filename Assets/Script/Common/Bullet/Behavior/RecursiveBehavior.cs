using UnityEngine;

namespace Script.Feature.Bullet {
public class RecursiveBehavior : IBulletBehavior {
    BulletPool _pool;
    public RecursiveBehavior(BulletPool pool) {
        _pool = pool;
    }
    public void OnImpact(Bullet bullet, Collision2D collision) {
        var contactPoint = collision.GetContact(0).point;

        var target = GetTransform(bullet);

        // bordeline monad
        Vector2 targetDir = target switch {
            null => Vector2.up,
            Transform t => ((Vector2)t.position - contactPoint).normalized
        };

        _pool.BulletRequest(contactPoint, targetDir)
             .WithDamage(bullet.Damage)
             .WithLifetime(bullet.Lifetime / 2)
             .WithSpeed(bullet.Speed)
             .WithTargetType(bullet.Type);
        Debug.Log("spawned ricoche bullet to: " + targetDir);
    }
    Transform GetTransform(Bullet bullet) {
        Collider2D[] hits = Physics2D.OverlapCircleAll(bullet.transform.position, 50f, bullet.gameObject.layer);
        if (hits.Length > 0) {
            return hits[0].transform;
        }
        return null;
    }
}
}
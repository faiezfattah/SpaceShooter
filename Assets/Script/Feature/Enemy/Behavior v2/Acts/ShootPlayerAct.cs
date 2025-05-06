using UnityEngine;

public class ShootPlayerAct : EnemyBehavior {
    float _bulletSpeed;
    public ShootPlayerAct(float bulletSpeed) {
        _bulletSpeed = bulletSpeed;
    }
    public override void OnUpdate(BehaviorActor actor) {
        Shoot(actor);
    }
    
    void Shoot(BehaviorActor actor) {
        actor.BulletPool.BulletRequest(actor.transform.position, actor.Direction)
                        .WithTargetType(EntityType.Player)
                        .WithDamage(1)
                        .WithSpeed(_bulletSpeed);
    }
}
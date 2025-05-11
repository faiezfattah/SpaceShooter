using System;
using Script.Core.Bullet;
using UnityEngine;

public class ShootToDirAct : EnemyBehavior {
    float _bulletSpeed;
    Func<IBulletConfig> _bullet = null;
    public ShootToDirAct(float bulletSpeed) {
        _bulletSpeed = bulletSpeed;
    }
    
    public ShootToDirAct(float bulletSpeed, Func<IBulletConfig> bullet) {
        _bulletSpeed = bulletSpeed;
        _bullet = bullet;
    }
    public override void OnFixedUpdate(BehaviorActor actor) {
        Shoot(actor);
    }
    
    void Shoot(BehaviorActor actor) {
        Debug.Log("shooting!");
        if (_bullet != null) {
            _bullet();
            
        } else {
            actor.BulletPool.BulletRequest(actor.transform.position, actor.Direction)
                            .WithTargetType(EntityType.Player)
                            .WithDamage(1)
                            .WithSpeed(_bulletSpeed);
        }
    }
}
using System;
using System.Collections;
using Script.Core.Bullet;
using UnityEngine;

public class ShootToDirAct : EnemyBehavior {
    float _bulletSpeed;
    Func<IBulletConfig> _bullet = null;
    float _interval;
    Coroutine _shootRoutine;
    BulletPattern _pattern;
    public ShootToDirAct(float bulletSpeed, float interval) {
        _bulletSpeed = bulletSpeed;
        _interval = interval;
    }
    
    public ShootToDirAct(float bulletSpeed, float interval, Func<IBulletConfig> bullet) {
        _bulletSpeed = bulletSpeed;
        _bullet = bullet;
        _interval = interval;
    }
    public ShootToDirAct(float bulletSpeed, float interval, BulletPattern pattern) {
        _bulletSpeed = bulletSpeed;
        _pattern = pattern;
        _interval = interval;
    }
    public override void OnEnter(BehaviorActor actor) {
        _shootRoutine = actor.StartCoroutine(ShootRoutine(actor));
    }
    public override void OnExit(BehaviorActor actor) {
        if (_shootRoutine != null) {
            actor.StopCoroutine(_shootRoutine);
            _shootRoutine = null;
        }
    }
    IEnumerator ShootRoutine(BehaviorActor actor) {
        while (true) {
            yield return _pattern.Init(
                actor.BulletPool,
                actor.Direction,
                actor.transform,
                actor.Damage,
                EntityType.Player
            );
            Debug.Log("enemy shot");
            yield return new WaitForSeconds(_interval);
        }
    }
    void Shoot(BehaviorActor actor) {
        Debug.Log("shooting!");
        if (_bullet != null) {
            _bullet();
        }
        else {
            actor.BulletPool.BulletRequest(actor.transform.position, actor.Direction)
                            .WithTargetType(EntityType.Player)
                            .WithDamage(1)
                            .WithSpeed(_bulletSpeed);
        }
    }
}
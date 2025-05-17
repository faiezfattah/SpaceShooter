using Script.Core.Bullet;
using UnityEngine;
using Script.Feature.Bullet;
public class MeteorBossActor : BehaviorActor {
    [SerializeField] float rotationSpeed = 10;
    [SerializeField] float bulletSpeed = 5;
    [SerializeField] float interval = 0.25f;
    void Awake() {
        WithHostileBehavior(
            new ParallelBehavior(
                new RotateDirectionModifier(rotationSpeed),
                new RotateToDirAct(),
                new ShootToDirAct(bulletSpeed, interval, BulletCall)
            )
        );
    }
    IBulletConfig BulletCall() {
        return BulletPool.BulletRequest(transform.position, Direction)
                         .WithBehaviour(new RecursiveBehavior(BulletPool))
                         .WithDamage(2)
                         .WithTargetType(EntityType.Player)
                         .WithSpeed(10)
                         .WithLifetime(5);
    }
}
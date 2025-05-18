using Script.Core.Bullet;
using UnityEngine;
using Script.Feature.Bullet;
public class MeteorBossActor : BehaviorActor {
    [SerializeField] float rotationSpeed = 10;
    [SerializeField] BulletPattern pattern;
    void Awake() {
        WithHostileBehavior(
            new ParallelBehavior(
                new RotateDirectionModifier(rotationSpeed),
                new RotateToDirAct(),
                new ShootToDirAct(pattern)
            )
        );
    }
}
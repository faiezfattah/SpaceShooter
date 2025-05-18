using System;
using System.Collections;
using UnityEngine;
public class ShooterActor : BehaviorActor {
    public float OrbitRange;
    public BulletPattern pattern;
    void Awake() {
        WithHostileBehavior(
            new ParallelBehavior(
                new DirectionToPlayerModifier(),
                new ConditionalModifer(new MoveToDirAct(), () => Vector3.Distance(Player.transform.position, transform.position) > OrbitRange),
                new RotateToDirAct(),
                new ShootToDirAct(pattern)
            )
        );
    }
}
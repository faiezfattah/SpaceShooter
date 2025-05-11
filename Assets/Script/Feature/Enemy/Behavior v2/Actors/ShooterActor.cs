using UnityEngine;
public class ShooterActor : BehaviorActor {
    public float BulletSpeed;
    public float OrbitRange;
    public float ShootInterval;
    void Awake() {
        WithHostileBehavior(
            new ParallelBehavior(
                new DirectionToPlayerModifier(),
                new ConditionalModifer(new MoveToDirAct(), () => Vector3.Distance(Player.transform.position, transform.position) > OrbitRange),
                new RotateToDirAct(),
                new ThrottleModifier(new ShootToDirAct(BulletSpeed), 3f, 0.02f)
            )
        );
    }
}
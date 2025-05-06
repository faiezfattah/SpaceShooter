using UnityEngine;
public class ShooterActor : BehaviorActor {
    public float BulletSpeed;
    public float OrbitRange;
    public float ShootInterval;
    void Awake() {
        WithHostileBehavior(
            new ParallelBehavior(
                new DirectionModifier(),
                new ConditionalModifer(new MoveToPlayerAct(), () => Vector3.Distance(Player.transform.position, transform.position) > OrbitRange),
                new RotateToPlayerAct(),
                new ThrottleModifier(new ShootPlayerAct(BulletSpeed), 3f, 0.02f)
            )
        );
    }
}
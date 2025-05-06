public class ShooterActor : BehaviorActor {
    public float BulletSpeed;
    void Awake() {
        WithHostileBehavior(
            new ParallelBehavior(
                new DirectionModifier(),
                new MoveToPlayerAct(),
                new RotateToPlayerAct(),
                new ThrottleModifier(new ShootPlayerAct(BulletSpeed), 3)
            )
        );
    }
}
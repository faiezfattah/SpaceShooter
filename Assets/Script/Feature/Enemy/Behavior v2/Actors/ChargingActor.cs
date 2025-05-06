using UnityEngine;

public class ChargingActor : BehaviorActor {
    [SerializeField] float chargeSpeed = 20;
    [SerializeField] float chargeAtDistance = 10;
    void Awake() {
        WithHostileBehavior(
            new ParallelBehavior(
                new DirectionModifier(),
                new ConditionalModifer(new MoveToPlayerAct(), () => Vector3.Distance(Player.transform.position, transform.position) >= chargeAtDistance),
                // new MoveToPlayerAct(),
                new RotateToPlayerAct(),
                new ThrottleModifier(new ChargeToPlayerAct(chargeSpeed), 5f, 1f))
        );
    }
}
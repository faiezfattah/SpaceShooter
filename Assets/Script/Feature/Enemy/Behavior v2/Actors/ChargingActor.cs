using UnityEngine;

public class ChargingActor : BehaviorActor {
    [SerializeField] float chargeMaxDuration = 1;
    [SerializeField] float chargeCooldown = 3;
    void Awake() {
        WithHostileBehavior(
            new ParallelBehavior(
                new ConditionalModifer(new DirectionToPlayerModifier(), () => MoveSpeed == 0),
                new MoveToDirAct(),
                new RotateToDirAct(),
                new ChargeToDirAct(chargeCooldown, chargeMaxDuration)
            )
        );
    }
}
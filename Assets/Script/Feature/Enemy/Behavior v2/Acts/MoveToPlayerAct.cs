using UnityEngine;

public class MoveToPlayerAct : EnemyBehavior {
    public override void OnUpdate(BehaviorActor actor) {
        actor.transform.position += actor.MoveSpeed * Time.deltaTime * actor.Direction;
    }
}
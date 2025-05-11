using UnityEngine;

public class MoveToDirAct : EnemyBehavior {
    public override void OnUpdate(BehaviorActor actor) {
        actor.transform.position += actor.MoveSpeed * Time.deltaTime * actor.Direction;
    }
}
public class DirectionToPlayerModifier : EnemyBehavior {
    public override void OnUpdate(BehaviorActor actor) {
        actor.Direction = (actor.Player.position - actor.transform.position).normalized;
    }
}
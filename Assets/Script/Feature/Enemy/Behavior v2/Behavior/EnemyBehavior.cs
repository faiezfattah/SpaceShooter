using UnityEngine;

public abstract class EnemyBehavior {
    public virtual void OnEnter(BehaviorActor actor) {}
    public virtual void OnUpdate(BehaviorActor actor) {}
    public virtual void OnFixedUpdate(BehaviorActor actor) {}
    public virtual void OnExit(BehaviorActor actor) {}
    public virtual void OnTriggerEnter2d(BehaviorActor actor, Collider2D collision) {}
    public virtual void OnCollisionEnter2d(BehaviorActor actor, Collision2D collision) {}
}

using System.Collections.Generic;
using UnityEngine;

public class ParallelBehavior : EnemyBehavior {
    List<EnemyBehavior> _behaviors = new();
    public ParallelBehavior(params EnemyBehavior[] enemyBehaviors) {
        _behaviors.AddRange(enemyBehaviors);
    }
    public ParallelBehavior AddBehavior(EnemyBehavior behavior) {
        _behaviors.Add(behavior);
        return this;
    }
    public override void OnEnter(BehaviorActor actor) {
        foreach (var b in _behaviors) {
            b.OnEnter(actor);
        }
    }
    public override void OnExit(BehaviorActor actor) {
        foreach (var b in _behaviors) {
            b.OnExit(actor);
        }
    }
    
    public override void OnUpdate(BehaviorActor actor) {
        foreach (var b in _behaviors) {
            b.OnUpdate(actor);
        }
    }
    public override void OnFixedUpdate(BehaviorActor actor) {
        foreach (var b in _behaviors) {
            b.OnFixedUpdate(actor);
        }
    }
    public override void OnTriggerEnter2d(BehaviorActor actor, Collider2D collider) {
        foreach (var b in _behaviors) {
            b.OnTriggerEnter2d(actor, collider);
        }
    }
    public override void OnCollisionEnter2d(BehaviorActor actor, Collision2D collision) {
        foreach (var b in _behaviors) {
            b.OnCollisionEnter2d(actor, collision);
        }
    }
}

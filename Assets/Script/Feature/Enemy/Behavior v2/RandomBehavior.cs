using System.Collections.Generic;
using UnityEngine;

public class RandomBehavior : EnemyBehavior {
    List<EnemyBehavior> _behaviors;
    EnemyBehavior _activeBehavior;
    EnemyBehavior GetBehavior() {
        return _behaviors[Random.Range(0, _behaviors.Count)];
    }
    public RandomBehavior AddBehavior(EnemyBehavior behavior) {
        _behaviors.Add(behavior);
        return this;
    }
    public override void OnEnter(BehaviorActor behavior) {
        _activeBehavior = GetBehavior();
        _activeBehavior.OnEnter(behavior);
    }
    public override void OnExit(BehaviorActor behavior) {
        _activeBehavior.OnExit(behavior);
        _activeBehavior = null;
    }
    
    public override void OnUpdate(BehaviorActor behavior) {
        _activeBehavior.OnUpdate(behavior);
    }
    public override void OnFixedUpdate(BehaviorActor behavior) {
        _activeBehavior.OnFixedUpdate(behavior);
    }
    public override void OnTriggerEnter2d(BehaviorActor behavior, Collider2D collider) {
        _activeBehavior.OnTriggerEnter2d(behavior, collider);
    }
    public override void OnCollisionEnter2d(BehaviorActor behavior, Collision2D collision) {
        _activeBehavior.OnCollisionEnter2d(behavior, collision);
    }
}

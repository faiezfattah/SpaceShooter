using System;

public class ConditionalModifer : EnemyBehavior {
    Func<bool> _condition;
    EnemyBehavior _enemyBehavior;
    public ConditionalModifer(EnemyBehavior enemyBehavior, Func<bool> condition) {
        _condition = condition;
        _enemyBehavior = enemyBehavior;
    }
    public override void OnUpdate(BehaviorActor actor) {
        if (_condition()) {
            _enemyBehavior.OnUpdate(actor);
        }
    }
    public override void OnFixedUpdate(BehaviorActor actor) {
        if (_condition()) {
            _enemyBehavior.OnFixedUpdate(actor);
        }
    }
}
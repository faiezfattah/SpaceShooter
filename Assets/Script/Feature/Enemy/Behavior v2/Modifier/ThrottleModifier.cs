using UnityEngine;

public class ThrottleModifier : EnemyBehavior {
    float _throttleDuration;
    float _timer;
    EnemyBehavior _enemyBehavior;
    public ThrottleModifier(EnemyBehavior enemyBehavior, float throttelDuration) {
        _throttleDuration = throttelDuration;
        _enemyBehavior = enemyBehavior;
        _timer = throttelDuration;
    }
    public override void OnUpdate(BehaviorActor actor) {
        _timer -= Time.deltaTime;

        if (_timer < 0) {
            _timer = _throttleDuration;
            _enemyBehavior.OnUpdate(actor);
        }
    }
    public override void OnFixedUpdate(BehaviorActor actor) {
        _timer -= Time.deltaTime;

        if (_timer < 0) {
            _timer = _throttleDuration;
            _enemyBehavior.OnFixedUpdate(actor);
        }
    }

}
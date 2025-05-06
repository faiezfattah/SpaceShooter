using UnityEngine;

public class ThrottleModifier : EnemyBehavior 
{
    private float _throttleDuration;
    private float _executionDuration;
    private float _throttleTimer;
    private float _executionTimer;
    private EnemyBehavior _enemyBehavior;

    public ThrottleModifier(EnemyBehavior enemyBehavior, float throttleDuration, float executionDuration = 0.001f)  {
        _enemyBehavior = enemyBehavior;
        _executionDuration = executionDuration;
        _throttleDuration = throttleDuration;
        _throttleTimer = 0f;
        _executionTimer = 0f;
    }

    public override void OnEnter(BehaviorActor actor)  {
        _throttleTimer = 0f;
        _executionTimer = _executionDuration;
        _enemyBehavior.OnEnter(actor);
    }

    public override void OnUpdate(BehaviorActor actor)  {
        if (_throttleTimer > 0) {
            _throttleTimer -= Time.deltaTime;
        }
        else if (_executionTimer > 0) {
            _executionTimer -= Time.deltaTime;
            _enemyBehavior.OnUpdate(actor);
            
            if (_executionTimer <= 0) {
                _throttleTimer = _throttleDuration;
                _executionTimer = _executionDuration;
            }
        }
    }

    public override void OnFixedUpdate(BehaviorActor actor)  {
        if (_throttleTimer > 0) {
            _throttleTimer -= Time.fixedDeltaTime;
        }
        else if (_executionTimer > 0) {
            _executionTimer -= Time.fixedDeltaTime;
            _enemyBehavior.OnFixedUpdate(actor);
            
            if (_executionTimer <= 0)  {
                _throttleTimer = _throttleDuration;
                _executionTimer = _executionDuration;
            }
        }
    }

    public override void OnExit(BehaviorActor actor) {
        _enemyBehavior.OnExit(actor);
    }

    public override void OnTriggerEnter2d(BehaviorActor actor, Collider2D collision) {
        _enemyBehavior.OnTriggerEnter2d(actor, collision);
    }

    public override void OnCollisionEnter2d(BehaviorActor actor, Collision2D collision) {
        _enemyBehavior.OnCollisionEnter2d(actor, collision);
    }
}
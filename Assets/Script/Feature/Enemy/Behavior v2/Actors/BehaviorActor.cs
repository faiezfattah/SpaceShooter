using System;
using System.Collections;
using Script.Feature.Bullet;
using UnityEngine;

public class BehaviorActor : MonoBehaviour {
    // shared variables move later...
    public Transform Player;
    public BulletPool BulletPool;

    // instance specific variables (can changed on each instance)
    public float MoveSpeed;
    public float AttackCooldown;
    public float AttackRange;
    public Vector3 Direction;
    // reference for acts
    [HideInInspector] public EnemyHealth health;
    
    // actor specific, created on builder
    EnemyBehavior _idleBehavior = new NullBehavior();
    EnemyBehavior _hostileBehavior = new NullBehavior();
    EnemyBehavior _awareBehavior = new NullBehavior();

    EnemyBehavior _current;
    [SerializeField]
    ReactiveProperty<EnemyState> _enemyState;
    [SerializeField]
    float _awareDuration = 5;
    IDisposable _subscription;
    Coroutine _awareCoroutine;
    internal void Start() {
        _enemyState = new(EnemyState.Idle);
        _current = _idleBehavior;
        _subscription = _enemyState.Subscribe(newstate => HandleChangeState(newstate));

        health = GetComponent<EnemyHealth>();
    }

    virtual public void Update() {
        _current.OnUpdate(this);
    }
    virtual public void FixedUpdate() {
        _current.OnFixedUpdate(this);
    }
    void OnTriggerEnter2D(Collider2D collision) {
        if (!collision.CompareTag("Player")) {
            return;
        }

        Player = collision.gameObject.transform;
        _current.OnTriggerEnter2d(this, collision);
        _enemyState.Value = EnemyState.Hostile;
    }
    void OnTriggerExit2D(Collider2D collision) {
        if (!collision.CompareTag("Player")) {
            return;
        }

        _enemyState.Value = EnemyState.Aware;
        if (_awareCoroutine != null) {
            StopCoroutine(_awareCoroutine);
        }
        _awareCoroutine = StartCoroutine(AwareInternal());
    }
    IEnumerator AwareInternal() {
        yield return new WaitForSeconds(_awareDuration);

        if (_enemyState.Value == EnemyState.Aware) {
            _enemyState.Value = EnemyState.Idle;
        }
        _awareCoroutine = null;
    }
    void OnCollisionEnter2D(Collision2D collision) {
        _current.OnCollisionEnter2d(this, collision);
    }
    void HandleChangeState(EnemyState newState) {
        Debug.Log("changing state");

        _current.OnExit(this);
        EnemyBehavior behavior = newState switch {
            EnemyState.Idle => _idleBehavior,
            EnemyState.Aware => _awareBehavior,
            EnemyState.Hostile => _hostileBehavior,
            _ => throw new ArgumentOutOfRangeException("state not possible")
        };
        behavior.OnEnter(this);
        _current = behavior;
    }
    void OnDisable() {
        _subscription.Dispose();
    }
    public enum EnemyState {
        Idle,
        Hostile,
        Aware,

    }
    #region builder methods
    public BehaviorActor WithIdleBehavior(EnemyBehavior behavior) {
        _idleBehavior = behavior;
        return this;
    }
    public BehaviorActor WithHostileBehavior(EnemyBehavior behavior) {
        _hostileBehavior = behavior;
        return this;
    }
    public BehaviorActor WithAwareBehavior(EnemyBehavior behavior) {
        _awareBehavior = behavior;
        return this;
    }
    #endregion
}


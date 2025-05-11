using System;
using UnityEngine;

public class BranchingModifier : EnemyBehavior {
    EnemyBehavior _onFalse;
    EnemyBehavior _onTrue;
    Func<bool> _condition;
    
    public BranchingModifier(EnemyBehavior onFalse, EnemyBehavior onTrue, Func<bool> condition) {
        _onFalse = onFalse;
        _onTrue = onTrue;
        _condition = condition;
    }
    
    public override void OnEnter(BehaviorActor actor) {
        if (_condition()) {
            _onTrue.OnEnter(actor);
        } else {
         _onFalse.OnEnter(actor);   
        }
    }
    public override void OnUpdate(BehaviorActor actor) {
        if (_condition()) {
            _onTrue.OnUpdate(actor);
        } else {
         _onFalse.OnUpdate(actor);
        }
    }
    public override void OnFixedUpdate(BehaviorActor actor) {
        if (_condition()) {
            _onTrue.OnFixedUpdate(actor);
        } else {
         _onFalse.OnFixedUpdate(actor);
        }
    }
    public override void OnExit(BehaviorActor actor) {
        if (_condition()) {
            _onTrue.OnExit(actor);
        } else {
         _onFalse.OnExit(actor);
        }
    }
    public override void OnTriggerEnter2d(BehaviorActor actor, Collider2D collider) {
        if (_condition()) {
            _onTrue.OnTriggerEnter2d(actor, collider);
        } else {
         _onFalse.OnTriggerEnter2d(actor, collider);   
        }
    }
    public override void OnCollisionEnter2d(BehaviorActor actor, Collision2D collision) {
        if (_condition()) {
            _onTrue.OnCollisionEnter2d(actor, collision);
        } else {
         _onFalse.OnCollisionEnter2d(actor, collision);   
        }
    }
}
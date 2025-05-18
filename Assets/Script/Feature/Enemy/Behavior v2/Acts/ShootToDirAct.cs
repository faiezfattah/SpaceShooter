using System;
using System.Collections;
using Script.Core.Bullet;
using UnityEngine;

public class ShootToDirAct : EnemyBehavior {
    Coroutine _shootRoutine;
    BulletPattern _pattern;
    public ShootToDirAct(BulletPattern pattern) {
        _pattern = pattern;
    }
    public override void OnEnter(BehaviorActor actor) {
        _shootRoutine = actor.StartCoroutine(ShootRoutine(actor));
    }
    public override void OnExit(BehaviorActor actor) {
        if (_shootRoutine != null) {
            actor.StopCoroutine(_shootRoutine);
            _shootRoutine = null;
        }
    }
    IEnumerator ShootRoutine(BehaviorActor actor) {
        while (true) {
            yield return _pattern.Init(
                actor.BulletPool,
                actor.Direction,
                actor.transform,
                actor.Damage,
                EntityType.Player
            );
            Debug.Log("enemy shot");
        }
    }
}
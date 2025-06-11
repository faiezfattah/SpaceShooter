using UnityEngine;

public class ChargeToDirAct : EnemyBehavior {
    private float _cooldownDuration;
    private float _chargeDuration;  
    private float _currentTimer; // change between keeping cooldown and duration.
    private float _moveSpeed; 
    private bool _isCharging;

    public ChargeToDirAct(float cooldown, float maxDuration) {
        _cooldownDuration = cooldown;
        _chargeDuration = maxDuration;
    }
    public override void OnEnter(BehaviorActor actor) {
        _moveSpeed = actor.MoveSpeed;

        actor.MoveSpeed = 0;
        _isCharging = false;
        _currentTimer = _cooldownDuration;
    }

    public override void OnUpdate(BehaviorActor actor) {
        _currentTimer -= Time.deltaTime;

        if (!_isCharging) { // if not charging and timer (cooldown) done
            if (_currentTimer <= 0) {
                StartCharge(actor);
            }
        }

        else { // if charging and timer (duration) ran out
            if (_currentTimer <= 0) {
                StopCharge(actor);
            }
        }
    }

    public override void OnCollisionEnter2d(BehaviorActor actor, Collision2D collision) {
        if (_isCharging)  {
            StopCharge(actor);
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(actor.Damage);
        }
         else {
             StopCharge(actor);
         }
    }
    private void StartCharge(BehaviorActor actor) {
        _isCharging = true;
        actor.MoveSpeed = _moveSpeed; 
        _currentTimer = _chargeDuration;
    }

    private void StopCharge(BehaviorActor actor){
        _isCharging = false;
        actor.MoveSpeed = 0;  
        _currentTimer = _cooldownDuration; 
    }
}
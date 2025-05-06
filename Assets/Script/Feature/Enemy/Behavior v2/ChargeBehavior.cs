using UnityEngine;

public class ChargeBehavior : EnemyBehavior {
    Vector3 _dir;
    float _chargeCooldown;
    public override void OnUpdate(BehaviorActor actor) {
        if (_chargeCooldown > 0) {
        Move(actor);
        Rotate(actor);
            _chargeCooldown -= Time.deltaTime;
        } else {
            _chargeCooldown = actor.AttackCooldown;
            Charge(actor);
        }
    }
    void Move(BehaviorActor actor) {
        _dir = (actor.Player.position - actor.transform.position).normalized;
        actor.transform.position += actor.MoveSpeed * Time.deltaTime * _dir;
    }
    void Rotate(BehaviorActor actor) {

        // other option is normlize the dir btw and use quaternion and set it to the transform.
        // adding this comment in case i need to animate it later.
        // or actually use the rotation speed variable. for now im lazy~
        // Quaternion rotation = Quaternion.LookRotation(Vector3.forward, dir);

        actor.transform.up = _dir;
    }
    void Charge(BehaviorActor actor) {

    }
}
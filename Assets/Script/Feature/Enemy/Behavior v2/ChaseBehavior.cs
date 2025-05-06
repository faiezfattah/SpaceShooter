using UnityEngine;

public class ChaseBehavior : EnemyBehavior {
    Vector3 _dir;
    float _shootCooldown;
    public override void OnUpdate(BehaviorActor actor) {
        _dir = (actor.Player.position - actor.transform.position).normalized;
        Rotate(actor);
        
        if (_shootCooldown > 0) {
            _shootCooldown -= Time.deltaTime;
        } 

        float dist = Vector3.Distance(actor.Player.position, actor.transform.position);
        
        if (dist >= actor.AttackRange) {
            Move(actor);
        }
        
        if (_shootCooldown <= 0 && dist < actor.AttackRange){
            _shootCooldown = actor.AttackCooldown;
            Shoot(actor);
        }
    }
    void Move(BehaviorActor actor) {
        actor.transform.position += actor.MoveSpeed * Time.deltaTime * _dir;
    }
    void Rotate(BehaviorActor actor) {

        // other option is normlize the dir btw and use quaternion and set it to the transform.
        // adding this comment in case i need to animate it later.
        // or actually use the rotation speed variable. for now im lazy~
        // Quaternion rotation = Quaternion.LookRotation(Vector3.forward, dir);

        actor.transform.up = _dir;
    }
    void Shoot(BehaviorActor actor) {
        actor.BulletPool.BulletRequest(actor.transform.position, _dir)
                        .WithTargetType(EntityType.Player)
                        .WithDamage(1)
                        .WithSpeed(actor.BulletSpeed);
    }
}
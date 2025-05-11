using UnityEngine;

public class RotateDirectionModifier : EnemyBehavior {
    float _speed;
    public RotateDirectionModifier(float rotationSpeed) {
        _speed = rotationSpeed;
    }
    public override void OnEnter(BehaviorActor actor) {
        actor.Direction = Vector3.up;
    }
    public override void OnUpdate(BehaviorActor actor){
        actor.Direction = Quaternion.Euler(0, 0, _speed) * actor.Direction;
    }
}
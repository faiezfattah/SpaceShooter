public class RotateToPlayerAct : EnemyBehavior {

    public override void OnFixedUpdate(BehaviorActor actor) {
        Rotate(actor);
    }
    void Rotate(BehaviorActor actor) {

        // other option is normlize the dir btw and use quaternion and set it to the transform.
        // adding this comment in case i need to animate it later.
        // or actually use the rotation speed variable. for now im lazy~
        // Quaternion rotation = Quaternion.LookRotation(Vector3.forward, dir);

        actor.transform.up = actor.Direction;
    }
}
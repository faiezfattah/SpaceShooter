using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Points To Target", story: "Points [self] to [Target]", category: "Action", id: "47b299634329e11e084237381631b785")]
public partial class PointsToTargetAction : Action {
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<float> RotationSpeed;

    protected override Status OnStart() {
        return Status.Running;
    }

    protected override Status OnUpdate() {
        Vector2 dir = Target.Value.transform.position - Self.Value.transform.position;

        // other option is normlize the dir btw and use quaternion and set it to the transform.
        // adding this comment in case i need to animate it later.
        // or actually use the rotation speed variable. for now im lazy~
        // Quaternion rotation = Quaternion.LookRotation(Vector3.forward, dir);

        Self.Value.transform.up = dir;
        
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}


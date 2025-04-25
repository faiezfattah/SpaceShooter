using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Shoot Target", story: "[Self] Shoot at [Target]", category: "Action", id: "0fd8e935830e9ba4ce3ebe1b1bf5ee65")]
public partial class ShootTargetAction : Action {
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    protected override Status OnStart() {
        return Status.Running;
    }

    protected override Status OnUpdate() {
        Debug.Log("shooting");
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}


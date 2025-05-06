using System;
using Unity.Behavior;
using UnityEngine;
using Modifier = Unity.Behavior.Modifier;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Distance Check", story: "Continue if [Self] and [Target] is Near [Distance]", category: "Flow", id: "2e2844ab45264dcc54c07464d7ceed38")]
public partial class DistanceCheckModifier : Modifier {
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<float> Distance;
    float sqrDistance => Distance.Value * Distance.Value;

    protected override Status OnStart() {
        return Status.Running;
    }

    protected override Status OnUpdate() {
        Vector2 vector = Target.Value.transform.position - Self.Value.transform.position;
        if (vector.sqrMagnitude <= sqrDistance) {
            return Status.Success;
        } else {
            return Status.Failure;
        }
        
    }

    protected override void OnEnd()
    {
    }
}


using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Move to Target", story: "Move [Self] to [Target]", category: "Action/Navigation", id: "f59d2eafff5c1fdab3a44f315206fa64")]
public partial class MoveToTargetAction : Action {
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<float> MoveSpeed;

    protected override Status OnStart() {
        return Status.Running;
    }

    protected override Status OnUpdate() {
        Vector3 dir = Target.Value.transform.position - Self.Value.transform.position;
        dir = dir.normalized;
        
        Self.Value.transform.position += MoveSpeed * Time.deltaTime * dir;
        return Status.Success;
    }

    protected override void OnEnd() {
    }
}


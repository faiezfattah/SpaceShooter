using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using Script.Feature.Bullet;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Shoot Target", story: "[Self] Shoot at [Target]", category: "Action", id: "0fd8e935830e9ba4ce3ebe1b1bf5ee65")]
public partial class ShootTargetAction : Action {
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<BulletPool> BulletPool;
    [SerializeReference] public BlackboardVariable<float> bulletSpeed;

    protected override Status OnStart() {
        return Status.Running;
    }

    protected override Status OnUpdate() {
        Vector2 direction = Target.Value.transform.position - Self.Value.transform.position;
        direction  = direction.normalized;

        BulletPool.Value.BulletRequest(Self.Value.transform.position, direction)
                        .WithSpeed(bulletSpeed)
                        .WithLifetime(2f);
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}


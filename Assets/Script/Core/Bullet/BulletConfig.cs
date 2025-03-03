using UnityEngine;

namespace Script.Core.Bullet {
public class BulletConfig {
    public Vector3 Position;
    public float Speed;
    public float Damage;
    public LayerMask Layer;
    
    // behaviour, pick one
    public Vector2 Direction = default;
    public Transform Target;
    
    // public BulletBehaviour Behaviour;
}
}
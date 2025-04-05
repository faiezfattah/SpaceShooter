using UnityEngine;

namespace Script.Core.Bullet {
public class BulletConfig {
    public Vector3 Position;
    public float Speed = 20f;
    public float Damage = 1f;
    public LayerMask Layer = 0;
    public Vector2 Direction = default;
    public BulletConfig(Vector3 position, Vector2 direction) {
        Position = position;
        Direction = direction;
    }
}
public static class BulletConfigExtensions {
    public static BulletConfig WithLayerMask(this BulletConfig config, LayerMask layerMask) {
        config.Layer = layerMask;
        return config;
    }
    
    public static BulletConfig WithSpeed(this BulletConfig config, float speed) {
        config.Speed = speed;
        return config;
    }
    
    public static BulletConfig WithDamage(this BulletConfig config, float damage) {
        config.Damage = damage;
        return config;
    }
}
}
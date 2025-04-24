using UnityEngine;

namespace Script.Core.Bullet {
public interface IBulletConfig {
    public IBulletConfig WithLayerMask(LayerMask layerMask);
    public IBulletConfig WithSpeed(float speed);
    public IBulletConfig WithDamage(float damage);
    public IBulletConfig WithLifetime(float lifetime);
}
}
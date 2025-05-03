using Script.Feature.Bullet;
using UnityEngine;

namespace Script.Core.Bullet {
public interface IBulletConfig {
    public IBulletConfig WithTargetType(EntityType type);
    public IBulletConfig WithSpeed(float speed);
    public IBulletConfig WithDamage(int damage);
    public IBulletConfig WithLifetime(float lifetime);
    public IBulletConfig WithBehaviour(IBulletBehaviour bulletBehaviour);
}
}
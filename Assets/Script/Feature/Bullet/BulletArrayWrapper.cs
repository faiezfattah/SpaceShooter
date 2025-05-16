using System;
using System.Collections;
using System.Numerics;
using Script.Core.Bullet;
using Script.Feature.Bullet;

public class BulletArrayCallbackWrapper : IBulletConfig {
    public IBulletConfig[] bullets;
    public BulletArrayCallbackWrapper(BulletPool bulletPool, int count) {
        bullets = bulletPool.RequestMultiple(count);
    }

    public IBulletConfig WithBehaviour(IBulletBehavior bulletBehaviour) {
        for (int i = 0; i < bullets.Length; i++) {
            bullets[i].WithBehaviour(bulletBehaviour);
        }

        return this;
    }

    public IBulletConfig WithDamage(int damage) {
        for (int i = 0; i < bullets.Length; i++) {
            bullets[i].WithDamage(damage);
        }

        return this;
    }

    public IBulletConfig WithLifetime(float lifetime) {
        for (int i = 0; i < bullets.Length; i++) {
            bullets[i].WithLifetime(lifetime);
        }

        return this;
    }

    public IBulletConfig WithSpeed(float speed) {
        for (int i = 0; i < bullets.Length; i++) {
            bullets[i].WithSpeed(speed);
        }

        return this;
    }

    public IBulletConfig WithTargetType(EntityType type) {
        for (int i = 0; i < bullets.Length; i++) {
            bullets[i].WithTargetType(type);
        }

        return this;
    }
}

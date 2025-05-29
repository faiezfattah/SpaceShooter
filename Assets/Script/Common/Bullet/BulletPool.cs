using System;
using Script.Core.Bullet;
using Script.Core.Pool;
using UnityEngine;
using UnityEngine.Pool;

namespace Script.Feature.Bullet {
public class BulletPool : Pool<Bullet> {
    public IBulletConfig BulletRequest(Vector3 position, Vector2 direction) {
        var bullet = _pool.Get();
        
        bullet.Setup(() => _pool.Release(bullet));

        bullet.transform.position = position;
        bullet.Direction = direction;
        
        return bullet;
    }
    public IBulletConfig[] RequestMultiple(int count) {
        var bullets = new IBulletConfig[count];

        for (int i = 0; i < count; i++) {
            bullets[i] = _pool.Get();
        }

        return bullets;    
    }
}
}
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
}
}
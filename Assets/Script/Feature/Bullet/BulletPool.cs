using System;
using Script.Core.Bullet;
using UnityEngine;
using UnityEngine.Pool;

namespace Script.Feature.Bullet {
public class BulletPool : MonoBehaviour, IBulletPool {
    [SerializeField] private Bullet bulletPrefab;
    private ObjectPool<Bullet> bulletPool;

    private void Awake() {
        bulletPool = new ObjectPool<Bullet>(
            CreateBullet, 
            OnGetBullet, 
            OnReleaseBullet, 
            OnDestroyBullet, 
            false, 
            10, 
            1_000);
    }

    private Bullet CreateBullet() {
        return Instantiate(bulletPrefab);
    }

    private void OnGetBullet(Bullet bullet) {
        bullet.gameObject.SetActive(true);
    }

    private void OnReleaseBullet(Bullet bullet) {
        bullet.Teardown();
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(Bullet bullet) {
        Destroy(bullet);
    }
    
    public void BulletRequest(BulletConfig bulletConfig) {
        var bullet = bulletPool.Get();
        
        bullet.Setup(() => bulletPool.Release(bullet));

        bullet.lifetime = 3f;
        bullet.transform.position = bulletConfig.Position;
        bullet.Speed = bulletConfig.Speed;
        bullet.gameObject.layer = bulletConfig.Layer;
        bullet.Damage = bulletConfig.Damage;

        if (bulletConfig.Direction != default) {
            bullet.Direction = bulletConfig.Direction;
        } else if (bulletConfig.Target != null) {
            bullet.Target = bulletConfig.Target;
        }
        else {
            throw new Exception("Trying to spawn bullet with no behaviour");
        }
    }
}
}
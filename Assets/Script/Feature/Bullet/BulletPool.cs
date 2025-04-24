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

    public IBulletConfig BulletRequest(Vector3 position, Vector2 direction) {
        var bullet = bulletPool.Get();
        
        bullet.Setup(() => bulletPool.Release(bullet));

        bullet.transform.position = position;
        bullet.Direction = direction;
        
        return bullet;
    }
}
}
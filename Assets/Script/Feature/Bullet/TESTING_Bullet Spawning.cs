using System;
using Script.Core.Bullet;
using Script.Feature.Input;
using UnityEngine;

namespace Script.Feature.Bullet {
public class TESTING_Bullet_Spawning : MonoBehaviour {
    [SerializeField] private BulletPool bulletPool;
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Transform target;
    

    private BulletConfig _bulletConfig;

    private void Start() {
        _bulletConfig = new BulletConfig(transform.position, Vector2.left);
    }

    private void Test() {
        bulletPool.BulletRequest(_bulletConfig);
    }
    private void OnEnable() {
        Debug.Log("init");
        inputReader.ShootingEvent += Test;
    }
    private void OnDisable() {
        inputReader.ShootingEvent -= Test;
    }
}
}
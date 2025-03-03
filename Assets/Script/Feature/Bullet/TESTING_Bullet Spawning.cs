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
    private BulletConfig _bulletConfig2;

    private void Start() {
        _bulletConfig = new BulletConfig() {
            Position = transform.position,
            Direction = new Vector2(1, 0),
            Speed = 20f,
            Layer = 0,
            Damage = 1f
        };
        _bulletConfig2 = new BulletConfig() {
            Target = target,
            Position = transform.position,
            Speed = 20f,
            Layer = 0,
            Damage = 1f
        };
    }

    private void Test() {
        bulletPool.BulletRequest(_bulletConfig);
    }
    private void Test2() {
        bulletPool.BulletRequest(_bulletConfig2);
    }
    private void OnEnable() {
        Debug.Log("init");
        inputReader.ShootingEvent += Test;
        inputReader.EscEvent += Test2;
    }
    private void OnDisable() {
        inputReader.ShootingEvent -= Test;
        inputReader.EscEvent -= Test2;
    }
}
}
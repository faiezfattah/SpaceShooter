using System;
using Script.Feature.Bullet;
using Script.Feature.Input;
using TriInspector;
using UnityEngine;

[RequireComponent(typeof(PlayerRotation))]
public class PlayerShooting : MonoBehaviour {
    [SerializeField] InputReader inputReader;
    [SerializeField] BulletPool bulletPool;
    [SerializeField] float shootInterval;
    [SerializeField] float bulletSpeed;
    [ReadOnly] bool canFire;
    [ReadOnly] float timer;
    PlayerRotation _playerRotation;
    IDisposable _subscription;

    // Update is called once per frame
    void Start() {
        _subscription = inputReader.ShootingEvent.Subscribe(ShootEvent);
    }
    void Update() {
        if(!canFire) {
            timer += Time.deltaTime;
            if(timer > shootInterval) {
                canFire = true;
                timer = 0;
            }
        }
    }

    void ShootEvent() {
        if (canFire) {
            bulletPool.BulletRequest(transform.position, _playerRotation.Dir)
                      .WithSpeed(bulletSpeed)
                      .WithTargetType(EntityType.Enemy)
                      .WithLifetime(1);
        }
    }

    void OnValidate() {
        _playerRotation = GetComponent<PlayerRotation>();
    }
    void OnDisable() {
        _subscription?.Dispose();
    }
}


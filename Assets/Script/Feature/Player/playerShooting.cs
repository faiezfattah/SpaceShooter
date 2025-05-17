using System;
using System.Collections;
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
    [SerializeField] float lifetime;
    [SerializeField] BulletPattern pattern;
    [Title("Debug---")]
    [ReadOnly] bool canFire;
    [ReadOnly] float timer;
    Coroutine _shootingRoutine;
    PlayerRotation _playerRotation;
    IDisposable _subscription;

    // Update is called once per frame
    void Start() {
        _playerRotation = GetComponent<PlayerRotation>();
        _subscription = inputReader.ShootingEvent.Subscribe(HandleShootEvent);
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

    void HandleShootEvent() {
        if (canFire && _shootingRoutine == null) {
            _shootingRoutine = StartCoroutine(ShootInternal());
            Debug.Log("shooting");
        }
    }
    IEnumerator ShootInternal() {
        yield return StartCoroutine(
            pattern.Init(bulletPool, _playerRotation.Dir, transform, 1, EntityType.Enemy)
        );
        _shootingRoutine = null;
    }
    void OnValidate() {
        _playerRotation = GetComponent<PlayerRotation>();
    }
    void OnDisable() {
        _subscription?.Dispose();
    }
}


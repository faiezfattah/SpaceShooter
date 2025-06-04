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
    //[SerializeField] float shootInterval; im following gemini to a tea for now -dhafin
    [SerializeField] BulletPattern currentEquippedPattern;
    [SerializeField] int damage = 5;
    [Title("Debug---")]
    Coroutine _shootingRoutine;
    PlayerRotation _playerRotation;
    SubscriptionBag _bag = new();
    public static ReactiveSubject<BulletPattern> OnBulletPatternChanged = new();
    [ShowInInspector]ReactiveProperty<bool> _isShooting = new(false);

    public float CurrentShootInterval => currentEquippedPattern != null ?
    currentEquippedPattern.cooldown : 0.2f;
    void Start() {
        _playerRotation = GetComponent<PlayerRotation>();
        bulletPool = FindAnyObjectByType<BulletPool>();
        inputReader.ShootingEvent.Subscribe(() => _isShooting.Value = true).AddTo(_bag);
        inputReader.ShootingEndEvent.Subscribe(() => _isShooting.Value = false).AddTo(_bag);
        _isShooting.Subscribe(HandleShootEvent).AddTo(_bag);
    }
    void HandleShootEvent(bool isShooting) {
        if (_shootingRoutine == null && isShooting) {
            _shootingRoutine = StartCoroutine(ShootInternal());
            Debug.Log("shooting");
        }
        if (!isShooting && _shootingRoutine != null) {
            StopCoroutine(_shootingRoutine);
            _shootingRoutine = null;
        }
    }
    IEnumerator ShootInternal() {
        do {
            yield return StartCoroutine(
                currentEquippedPattern.Init(bulletPool, _playerRotation.Dir, transform, damage, EntityType.Enemy)
            );
        } while (currentEquippedPattern.isStreamable);
    }

    public void SetPattern(BulletPattern newPattern) {

        Debug.Log($"PlayerShooting: Changing pattern from '{(currentEquippedPattern != null ? currentEquippedPattern.name : "None")}' to '{newPattern.name}'.");
        currentEquippedPattern = newPattern;

        if (_shootingRoutine != null) { 
            StopCoroutine(_shootingRoutine);
            _shootingRoutine = null;
        }
        OnBulletPatternChanged.Raise(newPattern);
    }
    void OnDisable() {
        _bag?.Dispose();
        if (_shootingRoutine != null) StopCoroutine(_shootingRoutine);
    }
}


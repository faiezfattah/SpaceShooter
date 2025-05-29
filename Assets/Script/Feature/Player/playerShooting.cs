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
    [Title("Debug---")]
    [ReadOnly] bool canFire = true;
    [ReadOnly] float timer;
    Coroutine _shootingRoutine;
    PlayerRotation _playerRotation;
    IDisposable _subscription;


    public float CurrentShootInterval => currentEquippedPattern != null ?
    currentEquippedPattern.cooldown : 0.2f;
    // Update is called once per frame
    void Start()
    {
        _playerRotation = GetComponent<PlayerRotation>();
        _subscription = inputReader.ShootingEvent.Subscribe(HandleShootEvent);
        bulletPool = FindAnyObjectByType<BulletPool>();
        
    }
    void Update() {
        if(!canFire) {
            timer += Time.deltaTime;
            if(timer > CurrentShootInterval) {
                canFire = true;
                //timer = 0; gemini
            }
        }
    }

    void HandleShootEvent() {
        if (canFire && _shootingRoutine == null) {
            _shootingRoutine = StartCoroutine(ShootInternal());
            canFire = false;
            timer = 0;
            Debug.Log("shooting");
        }
    }
    IEnumerator ShootInternal() {
        yield return StartCoroutine(
            currentEquippedPattern.Init(bulletPool, _playerRotation.Dir, transform, 1, EntityType.Enemy)
        );
        _shootingRoutine = null;
    }

     public void SetPattern(BulletPattern newPattern) {

        Debug.Log($"PlayerShooting: Changing pattern from '{(currentEquippedPattern != null ? currentEquippedPattern.name : "None")}' to '{newPattern.name}'.");
        currentEquippedPattern = newPattern;

        canFire = true; 
        timer = 0;      

        if (_shootingRoutine != null) { 
            StopCoroutine(_shootingRoutine);
            _shootingRoutine = null;
        }
    }
    void OnValidate() {
        _playerRotation = GetComponent<PlayerRotation>();
    }
    void OnDisable() {
        _subscription?.Dispose();
    }
}


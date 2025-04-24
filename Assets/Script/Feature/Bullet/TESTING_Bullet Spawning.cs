using System;
using Script.Core.Bullet;
using Script.Feature.Input;
using UnityEngine;

namespace Script.Feature.Bullet {
public class TESTING_Bullet_Spawning : MonoBehaviour {
    [SerializeField] private BulletPool bulletPool;
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Transform target;
    

    private void Start() {
    }

    private void Test() {
        bulletPool.BulletRequest(transform.position, Vector2.left);
        Debug.Log("bullet spawning!");
    }
    private void OnEnable() {
        inputReader.ShootingEvent += Test;
    }
    private void OnDisable() {
        inputReader.ShootingEvent -= Test;
    }
}
}
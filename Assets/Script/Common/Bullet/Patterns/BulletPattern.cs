using System;
using System.Collections;
using Script.Core.Bullet;
using Script.Feature.Bullet;
using UnityEngine;

// [CreateAssetMenu(fileName = "New Bullet Pattern", menuName = "Bullet Pattern/New Pattern")]
public abstract class BulletPattern : ScriptableObject {
    // return the array and the coroutine
    public float bulletSpeed = 10;
    public float bulletLifetime = 1;
    public float cooldown = 2f;
    public bool isStreamable = false;
    public float damageMultiplier = 1;
    public abstract IEnumerator Init(
        BulletPool bulletPool,
        Vector2 direction,
        Transform shooter,
        int damage,
        EntityType targetType,
        IBulletBehavior behavior = null);
    /*
    public abstract Execute(BulletPool, Vector2, Vector3, Lifetime, Type, behavior?)
    
    caller: type, lifetime, behavior? dir, bullet pool
    public IBulletConfig WithTargetType(EntityType type); -> by caller
    public IBulletConfig WithSpeed(float speed); -> defined by the pattern
    public IBulletConfig WithDamage(int damage); -> defined by the caller
    public IBulletConfig WithLifetime(float lifetime); -> defined by the pattern
    public IBulletConfig WithBehaviour(IBulletBehavior bulletBehaviour); -> by pattern?
    
    */
}


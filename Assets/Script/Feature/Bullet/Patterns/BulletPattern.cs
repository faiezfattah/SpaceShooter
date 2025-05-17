using System;
using System.Collections;
using Script.Core.Bullet;
using Script.Feature.Bullet;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletPattern", menuName = "Scriptable Objects/BulletPattern")]
public abstract class BulletPattern : ScriptableObject {
    // return the array and the coroutine
    public abstract IEnumerator Init(BulletPool bulletPool, Vector2 dir, Transform pos, int damage, EntityType type, IBulletBehavior behavior = null);
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


using System;
using System.Collections;
using Script.Core.Bullet;
using Script.Feature.Bullet;
using UnityEngine;

// [CreateAssetMenu(fileName = "BulletPattern", menuName = "Scriptable Objects/BulletPattern")]
public abstract class BulletPattern : ScriptableObject {
    // return the array and the coroutine
    public abstract (IBulletConfig configHandle, Func<Vector3, Vector2, IEnumerator>  execute) Init(BulletPool bulletPool);
}


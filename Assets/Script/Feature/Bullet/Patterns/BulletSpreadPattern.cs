using System;
using System.Collections;
using Script.Core.Bullet;
using Script.Feature.Bullet;
using UnityEngine;

public class BulletSpreadPattern : BulletPattern {
    [SerializeField] int bulletCount = 3;
    [SerializeField] float spreadAngle = 15f;
    public override (IBulletConfig configHandle, Func<Vector3, Vector2, IEnumerator>  execute) Init(BulletPool bulletPool) {
        var bullet = new BulletArrayCallbackWrapper(bulletPool, bulletCount);

        return (bullet, (pos, dir) => Execute(bullet, pos, dir));
    }

    public IEnumerator Execute(BulletArrayCallbackWrapper bulletArray, Vector3 pos, Vector2 dir) {
        for (int i = 0; i < bulletArray.bullets.Length; i++) {
            // some code
        }
        yield return null;
    }
}
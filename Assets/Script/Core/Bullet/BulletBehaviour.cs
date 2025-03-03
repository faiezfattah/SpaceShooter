using UnityEngine;

namespace Script.Core.Bullet {
public abstract class BulletBehaviour : ScriptableObject {
    // note: create bullet context that provide all the data that every behaviour could need
    public abstract void Execute(Feature.Bullet.Bullet bullet);
}
}
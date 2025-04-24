using UnityEngine;

namespace Script.Core.Bullet {
public interface IBulletPool {
    public IBulletConfig BulletRequest(Vector3 position, Vector2 direction);
}
}
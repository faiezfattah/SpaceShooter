using UnityEngine;
namespace Script.Feature.Bullet {

    // so there is 3 type of things that can be modified
    // 1. the movement
    // 2. the spawning  let skip this one?
    // 3. the impact

    // each behaviour need different things.
    // honing may need Transform

public interface IBulletBehaviour {
    public void OnInit(Bullet bullet) {}
    public void OnMove(Bullet bullet) {}
    public void OnImpact(Bullet bullet, Collision2D collision) {}
}
}
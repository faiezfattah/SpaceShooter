using UnityEngine;

namespace Script.Feature.Bullet {
public class HomingBehavior : IBulletBehavior {
    float _timerToChase;
    float _detectionRadius;
    Transform _target;
    bool _casted;
    public HomingBehavior(float timeToChase, float detectionRadius = 20) {
        _timerToChase = timeToChase;
        _detectionRadius = detectionRadius;
    }
    public void OnInit(Bullet bullet) {

    }

    public void OnMove(Bullet bullet) {
        if (_timerToChase > 0 ) _timerToChase -= Time.deltaTime;

        if (_timerToChase <= 0) {
            if (!_target && !_casted) {
                _target = GetTransform(bullet);
                _casted = true; // stop it from getting transform each frame.
            } else {
                var dir = (_target.position - bullet.transform.position).normalized; // optimization note
                bullet.Direction = dir;
            }
        } 
    }
    Transform GetTransform(Bullet bullet) {
        Collider2D[] hits = Physics2D.OverlapCircleAll(bullet.transform.position, _detectionRadius, bullet.gameObject.layer);
        if (hits.Length > 0) {
            return hits[0].transform;
        }
        return null;
    }
}
}

using UnityEngine;

namespace Script.Feature.Bullet {
public class HoningBehavior : IBulletBehaviour {
    float _timerToHone;
    float _detectionRadius;
    Transform _target;
    public HoningBehavior(float timeToHone, float detectionRadius) {
        _timerToHone = timeToHone;
        _detectionRadius = detectionRadius;
    }
    public void OnInit(Bullet bullet) {

    }

    public void OnMove(Bullet bullet) {
        if (_timerToHone > 0 ) _timerToHone -= Time.deltaTime;

        if (_timerToHone <= 0) {
            if (!_target) {
                _target = GetTransform(bullet);
            } else {
                var dir = (_target.position - bullet.transform.position).normalized;
                bullet.Direction = dir;
            }
        } 
    }
    Transform GetTransform(Bullet bullet) {
        Collider2D[] hits = Physics2D.OverlapCircleAll(bullet.transform.position, _detectionRadius, bullet.gameObject.layer);
        if (hits.Length > 0) {
            Debug.Log("target was found");
            return hits[0].transform;
        }
            Debug.LogWarning("target NOT was found");
        return null;
    }
}
}

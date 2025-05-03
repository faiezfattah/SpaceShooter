using System;
using JetBrains.Annotations;
using Script.Core.Bullet;
using Script.Core.Pool;
using UnityEngine;

namespace Script.Feature.Bullet {
[RequireComponent(typeof(Collider2D))]
public class Bullet : MonoBehaviour, IBulletConfig, IPoolable {
    Action _onRelease;

    // accesible variables for behaviour
    public float Lifetime {private set; get;} = 5f; 
    public float Speed {private set; get;}  = 5f;
    public int Damage {private set; get;}  = 1;
    public EntityType Type  {private set; get;}
    public Transform Target;

    // accesible variable that changes the bullet it self.
    public Vector2 Direction = default;

    IBulletBehavior _behaviour;

    void FixedUpdate() {
        _behaviour?.OnMove(this);

        if (Direction != default) {
            transform.position += (Vector3) Direction * (Speed * Time.deltaTime); // optimization note just in case
        }

        Lifetime -= Time.deltaTime; // optimization note again, just in case
        if (Lifetime <= 0f) _onRelease?.Invoke();
        
    }
    public void Setup(Action onRelease) => _onRelease = onRelease;
    public void Reset() {
        _onRelease = null;
        
        Speed = 5f;
        Damage = 1;
        Lifetime = 5f;
        
        Direction = default;
        _behaviour = null;
    }

    void OnCollisionEnter2D(Collision2D other) {
        // todo: apply damage here

        if (other.gameObject.TryGetComponent<Health>(out var h)) {
            h.TakeDamage(Damage);
        }

        _behaviour?.OnImpact(this, other);
        _onRelease?.Invoke();
    }

    #region builder methods
    public IBulletConfig WithTargetType(EntityType type = EntityType.All) {
        gameObject.layer = type.GetDamagingMask();
        Type = type;
        
        return this;
    }
    
    public IBulletConfig WithSpeed(float speed) {
        Speed = speed;
        return this;
    }
    
    public IBulletConfig WithDamage(int damage) {
        Damage = damage;
        return this;
    }
    public IBulletConfig WithLifetime(float lifetime = 5f) {
        Lifetime = lifetime;
        return this;
    }
    public IBulletConfig WithBehaviour(IBulletBehavior bulletBehaviour)    {
        _behaviour = bulletBehaviour;
        _behaviour.OnInit(this);
        return this;
    }
    #endregion
}
}
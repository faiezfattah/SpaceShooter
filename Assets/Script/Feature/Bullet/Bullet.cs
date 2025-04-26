using System;
using JetBrains.Annotations;
using Script.Core.Bullet;
using UnityEngine;

namespace Script.Feature.Bullet {
[RequireComponent(typeof(Collider2D))]
public class Bullet : MonoBehaviour, IBulletConfig {
    Action _onRelease;

    [SerializeField] float lifetime = 5f;
    [SerializeField] float speed = 5f;
    [SerializeField] float damage = 1f;
    public Vector2 Direction;
    [CanBeNull] public Transform Target;

    // public BulletBehaviour Behaviour;

    void FixedUpdate() {
        if (Direction != Vector2.zero) {
            transform.position += (Vector3) Direction * (speed * Time.deltaTime);
        }
        else {
            transform.position += Target.position * (speed * Time.deltaTime);
        }
        lifetime -= Time.deltaTime;
        if (lifetime <= 0f) _onRelease?.Invoke();
        
    }
    public void Setup(Action onRelease) => _onRelease = onRelease;
    public void Teardown() {
        _onRelease = null;
        
        speed = 5f;
        damage = 1f;
        lifetime = 5f;
        
        Direction = Vector2.zero;
        Target = null;
    }

    void OnCollisionEnter2D(Collision2D other) {
        // todo: apply damage here
        _onRelease?.Invoke();
        Debug.Log("hit1");
    }

    #region builder methods
    public IBulletConfig WithLayerMask(LayerMask layerMask) {
        gameObject.layer = layerMask;
        return this;
    }
    
    public IBulletConfig WithSpeed(float speed) {
        this.speed = speed;
        return this;
    }
    
    public IBulletConfig WithDamage(float damage) {
        this.damage = damage;
        return this;
    }
    public IBulletConfig WithLifetime(float lifetime = 5f) {
        this.lifetime = lifetime;
        return this;
    }
    #endregion
}
}
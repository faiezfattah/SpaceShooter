using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Script.Feature.Bullet {
[RequireComponent(typeof(Collider2D))]
public class Bullet : MonoBehaviour {
    private Action _onRelease;

    public float lifetime = 5f;
    public float Speed;
    public float Damage;
    public Vector2 Direction;
    [CanBeNull] public Transform Target;

    // public BulletBehaviour Behaviour;

    private void FixedUpdate() {
        if (Direction != Vector2.zero) {
            transform.position += (Vector3) Direction * (Speed * Time.deltaTime);
        }
        else {
            transform.position += Target.position * (Speed * Time.deltaTime);
        }
        lifetime -= Time.deltaTime;
        if (lifetime <= 0f) _onRelease?.Invoke();
        
        // Behaviour.Execute(this);
    }
    public void Setup(Action onRelease) => _onRelease = onRelease;
    public void Teardown() {
        _onRelease = null;
        
        Speed = 0;
        Damage = 0;
        
        Direction = Vector2.zero;
        Target = null;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        // apply damage here
        _onRelease?.Invoke();
        Debug.Log("hit1");
    }
}
}
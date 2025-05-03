using UnityEngine;
using Script.Feature.Input;
using UnityEngine.InputSystem;
using System;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] float terminalVelocity = 20;
    [SerializeField] float acceleration = 2;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] InputReader inputReader;
    public Vector2 Dir;
    IDisposable _subscription;
    float _currentSpeed;
    // float currentSpeed; // linearVelocity's magnitute

    void FixedUpdate() {
        Move();
    }
    void Move() {
        if (Dir == default) {
            _currentSpeed = 0;
            return;
        }

        _currentSpeed = Mathf.Clamp(_currentSpeed + acceleration, 0, terminalVelocity);

        rb.linearVelocity = _currentSpeed * Dir;
    }

    void OnMove(Vector2 value) {
        Dir = value;
    }
    void OnEnable() {
        _subscription = inputReader.MoveEvent.Subscribe(OnMove);
    }

    void OnDisable() {
        _subscription.Dispose();
    }
}
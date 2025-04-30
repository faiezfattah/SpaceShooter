using UnityEngine;
using Script.Feature.Input;
using UnityEngine.InputSystem;
using System;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] float terminalVelocity = 20;
    [SerializeField] float acceleration = 2;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] InputReader inputReader;
    [SerializeField] Camera _mainCam;
    [Header("Debug")]
    [SerializeField] float currentVelocity;
    public Vector2 dir;
    Mouse _mouse;
    float _currentSpeed;
    // float currentSpeed; // linearVelocity's magnitute
    void Start() {
        _mouse = Mouse.current;
        _mainCam ??= Camera.main;
    }

    void FixedUpdate() {
        Move();
        Rotate();
    }
    void Move() {
        if (dir == default) {
            _currentSpeed = 0;
            return;
        }

        _currentSpeed = Mathf.Clamp(_currentSpeed + acceleration, 0, terminalVelocity);

        rb.linearVelocity = _currentSpeed * dir;

        // todo: debug, remove later
        currentVelocity = _currentSpeed;
    }

    void Rotate() {
        Vector2 mouseScreenPosition = _mouse.position.ReadValue();

        Vector3 mouseWorldPosition = _mainCam.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, 0f));
        Vector2 direction = (mouseWorldPosition - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);

        transform.rotation = rotation;
    }

    void OnMove(Vector2 value) {
        dir = value;
    }
    void OnEnable() {
        inputReader.MoveEvent += OnMove;
    }

    void OnDisable() {
        inputReader.MoveEvent -= OnMove;
    }
}
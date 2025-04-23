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
    Vector2 _moveInput;
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
        if (_moveInput == default) {
            _currentSpeed = 0;
            return;
        }

        _currentSpeed = Mathf.Clamp(_currentSpeed + acceleration, 0, terminalVelocity);

        rb.linearVelocity = _currentSpeed * _moveInput;

        currentVelocity = _currentSpeed;
    }

    void Rotate() {
        Vector2 mouseScreenPosition = _mouse.position.ReadValue();

        Vector3 mouseWorldPosition = _mainCam.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, 0f));
        Vector2 direction = (mouseWorldPosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // todo: dont use atan2

        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
    }

    void OnMove(Vector2 value) {
        _moveInput = value;
    }
    void OnEnable() {
        inputReader.MoveEvent += OnMove;
    }

    void OnDisable() {
        inputReader.MoveEvent -= OnMove;
    }
}
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Script.Feature.Input {
[CreateAssetMenu(fileName = "InputReader", menuName = "Scriptable Objects/InputReader")]
public class InputReader : ScriptableObject, PlayerInput.IDefaultActions {
    private PlayerInput _input;

    ReactiveSubject _shootingEvent = new();
    ReactiveSubject _shootingEndEvent = new();
    ReactiveSubject _escEvent = new();
    ReactiveSubject<Vector2> _moveEvent = new(); // note: this is normalized vector 
    ReactiveSubject _debugSubject = new();

    public IReactive ShootingEvent => _shootingEvent;
    public IReactive ShootingEndEvent => _shootingEndEvent;
    public IReactive EscEvent => _escEvent;
    public IReactive<Vector2> MoveEvent => _moveEvent; 
    public IReactive DebugEvent => _debugSubject;
    
    
    public void OnShoot(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Performed) {
            _shootingEvent?.Raise();
        }
        if (context.phase == InputActionPhase.Canceled) {
                _shootingEndEvent?.Raise();
        }
    }
    public void OnEsc(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Performed) {
            _escEvent?.Raise();
        }
    }

    public void OnMove(InputAction.CallbackContext context) {
        _moveEvent?.Raise(context.ReadValue<Vector2>());
    }
    public void OnDebug(InputAction.CallbackContext context) {
        if (context.performed) _debugSubject.Raise();
    }
    private void OnEnable() {
        Debug.Log("input init");
        _input ??= new PlayerInput();
        _input.Enable();
        _input.Default.Enable();
        _input.Default.SetCallbacks(this);
    }
    private void OnDisable() {
        _input.Disable();
    }
}
}
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Script.Feature.Input {
[CreateAssetMenu(fileName = "InputReader", menuName = "Scriptable Objects/InputReader")]
public class InputReader : ScriptableObject, PlayerInput.IDefaultActions {
    private PlayerInput _input;

    public Action ShootingEvent;
    public Action EscEvent;
    
    public void OnShoot(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Performed) {
            ShootingEvent?.Invoke();
        }
    }
    public void OnEsc(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Performed) {
            EscEvent?.Invoke();
        }
    }

    private void OnEnable() {
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
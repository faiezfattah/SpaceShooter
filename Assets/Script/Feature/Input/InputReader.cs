using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Script.Feature.Input {
[CreateAssetMenu(fileName = "InputReader", menuName = "Scriptable Objects/InputReader")]
public class InputReader : ScriptableObject, PlayerInput.IDefaultActions {
    private PlayerInput _input;

    public Action ShootingEvent;
    public Action EscEvent;
    public Action<Vector2> MoveEvent; // note: this is normalized vector 
    
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

    public void OnMove(InputAction.CallbackContext context) {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
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
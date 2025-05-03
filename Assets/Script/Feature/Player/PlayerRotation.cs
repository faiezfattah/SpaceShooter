using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotation : MonoBehaviour {
    public Vector2 Dir;
    Camera _mainCam;
    Mouse _mouse;
    void Start() {
        _mouse = Mouse.current;
        _mainCam ??= Camera.main;
    }
    void FixedUpdate() {
        Rotate();
    }
    void Rotate() {
        Vector2 mouseScreenPosition = _mouse.position.ReadValue();

        Vector3 mouseWorldPosition = _mainCam.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, 0f));
        Dir = (mouseWorldPosition - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, Dir);

        transform.rotation = rotation;
    }
}
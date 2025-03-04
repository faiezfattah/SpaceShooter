using UnityEngine;
using Script.Feature.Input;

public class playerMovement : MonoBehaviour {
    public float moveSpeed;
    public Rigidbody2D rb;
    public InputReader inputReader;
    private Vector2 moveInput;

    private void OnEnable() {
        inputReader.MoveEvent += OnMove;
    }

    private void OnDisable() {
        inputReader.MoveEvent -= OnMove;
    }

    private void OnMove(Vector2 value) {
        moveInput = value;
        Debug.Log("Move: " + value);
    }

    private void FixedUpdate() {
        if (moveInput != Vector2.zero) {
            rb.linearVelocity = moveInput * (moveSpeed * Time.fixedDeltaTime);
        }

        Rotate();
    }

    void Rotate()
    {
        float inputX, inputY;
        inputX = Input.GetAxis("Mouse X");
        inputY = Input.GetAxis("Mouse Y");

        transform.Rotate(0, 0, -inputX * 10f);
    }

}
using UnityEngine;

public class spaceship : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;

    void Start()
    {
        rb.gravityScale = 0f;
    }

    void Update()
    {
        float moveY = Input.GetAxisRaw("Vertical");
        rb.linearVelocity = new Vector2(0, moveY * moveSpeed);
    }

}

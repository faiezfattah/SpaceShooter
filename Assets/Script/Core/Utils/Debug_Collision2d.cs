using UnityEngine;

public class Debug_Collision2d : MonoBehaviour {
    void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log(gameObject.name + " HIT: " + collision.gameObject.name);
    }
}
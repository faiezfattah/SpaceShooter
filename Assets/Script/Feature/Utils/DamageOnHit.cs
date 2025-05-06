using UnityEngine;

public class DamageOnHit : MonoBehaviour {
    [SerializeField] int damage;
    private PlayerHealth _playerHealth; 
    void Start() {
        _playerHealth = FindAnyObjectByType<PlayerHealth>(); 

        if (_playerHealth == null) {
            Debug.LogError("player health not found");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            _playerHealth.TakeDamage(damage);
        }
    }
}

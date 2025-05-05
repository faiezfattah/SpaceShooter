using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnHit : MonoBehaviour {
    public static PlayerHealth playerHealth;
    [SerializeField] int damage;
    void Start()
    {
        playerHealth ??= FindAnyObjectByType<PlayerHealth>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerHealth.TakeDamage(damage);
        }
    }
}

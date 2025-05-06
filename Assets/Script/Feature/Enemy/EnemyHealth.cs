using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public ReactiveProperty<int> currentHealth;
    [SerializeField] int maxHealth;
    void Start()
    {
        currentHealth = new(maxHealth);
    }

    public void TakeDamage(int amount)
    {
        currentHealth.Value -= amount;
        if (currentHealth.Value <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void HealthPickup(int amount){
        
        if (currentHealth.Value <= maxHealth){
            currentHealth.Value += amount; 
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    
    public int currentHealth;
    [SerializeField] public int maxHealth;
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void HealthPickup(int amount){
        
        if(currentHealth <= maxHealth){
            currentHealth += amount; 
        }
    }

}

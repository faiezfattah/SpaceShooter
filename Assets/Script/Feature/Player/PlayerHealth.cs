using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
     public int currentHealth;
    [SerializeField] public int maxHealth=5;
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

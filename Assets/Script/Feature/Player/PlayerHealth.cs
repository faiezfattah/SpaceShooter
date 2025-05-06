using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private static ReactiveProperty<int> currentHealth;
    [SerializeField] int maxHealth = 5;
    public static IReactive<int> CurrentPlayerHealth => currentHealth;
    void Start()
    {
        currentHealth = new(maxHealth);
    }

    public void TakeDamage(int amount)
    {
        currentHealth.Value -= amount;
        if(currentHealth.Value <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void HealthPickup(int amount){
        
        if(currentHealth.Value <= maxHealth){
            currentHealth.Value += amount; 
        }
    }

}

using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 5;

    static ReactiveProperty<int> currentHealth;
    public static IReactive<int> CurrentPlayerHealth => currentHealth;

    public static ReactiveSubject PlayerDeath = new();
    void Start()
    {
         currentHealth = new ReactiveProperty<int>(maxHealth);
    }

    public void TakeDamage(int amount)
    {
        currentHealth.Value -= amount;
        AudioSystem.Instance.PlaySfx(AudioSystem.Instance.hitClip);
        // Debug.Log("player damaged"+amount+" new health: " + currentHealth.Value);

        if (currentHealth.Value <= 0)
        {
            AudioSystem.Instance.PlaySfx(AudioSystem.Instance.deathClip);
            PlayerDeath.Raise();
            Destroy(gameObject); // 💀 Ilang
        }
    }


    public void HealthPickup(int amount){
        
        if(currentHealth.Value <= maxHealth){
            currentHealth.Value += amount; 
        }
    }

}

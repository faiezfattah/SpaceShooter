using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private static ReactiveProperty<int> currentHealth;

    public static int MaxHealth;
    [SerializeField] int maxHealth = 5;
    public static IReactive<int> CurrentPlayerHealth => currentHealth;
    void Start()
    {
         currentHealth = new ReactiveProperty<int>(maxHealth);
          MaxHealth = maxHealth;
    }

    public void TakeDamage(int amount)
{
    currentHealth.Value -= amount;
    AudioSystem.Instance.PlaySfx(AudioSystem.Instance.hitClip); 

    if(currentHealth.Value <= 0)
    {
        AudioSystem.Instance.PlaySfx(AudioSystem.Instance.deathClip); 
        Destroy(gameObject); // 💀 Ilang
    }
}


    public void HealthPickup(int amount){
        
        if(currentHealth.Value <= maxHealth){
            currentHealth.Value += amount; 
        }
    }

}

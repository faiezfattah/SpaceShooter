
using UnityEngine;
using System; 
using Script.Core.Pool; 

public class Item : MonoBehaviour, IPoolable 
{
    // Define the different kinds of items we can have
    public enum ItemType { XPPickup, ShieldPickup, HealthPickup}

    [Header("Item Configuration")]
    [SerializeField] private ItemType type = ItemType.XPPickup;
    [SerializeField] private SpriteRenderer itemSpriteRenderer; 

    [Header("Type Specific Settings")]
    [SerializeField] private int xpAmount = 10; 
    [SerializeField] private float shieldDuration = 5f; 
    [SerializeField] private int healthAmount = 1; 



    [Header("Visuals")]
    [SerializeField] private Sprite xpSprite; 
    [SerializeField] private Sprite shieldSprite; 
    [SerializeField] private Sprite healthSprite; 


    private Action _releaseAction; 

  

    public void Setup(Action releaseAction)
    {
       
        _releaseAction = releaseAction;
        ConfigureVisuals();
    }

    public void Reset()
    {
    
        _releaseAction = null;

    }


    void Awake()
    {

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
        } else {
      
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.gravityScale = 0;
            Debug.LogWarning($"Rigidbody2D was missing on {gameObject.name}, added one and set to Kinematic.");
        }

        // Ensure Collider is set correctly
        Collider2D col = GetComponent<Collider2D>();
        if(col == null) {
            Debug.LogError($"Collider2D is missing on {gameObject.name}! Pickup won't work.", this);
        } else if (!col.isTrigger) {
             Debug.LogWarning($"Collider2D on {gameObject.name} was not set to 'Is Trigger'. Setting it now.", this);
             col.isTrigger = true;
        }

        
    }

    // Set the sprite based on the item type
    private void ConfigureVisuals()
    {
        if (itemSpriteRenderer == null)
        {
            Debug.LogWarning($"Item Sprite Renderer not assigned on {gameObject.name}. Cannot set visual.", this);
            return;
        }

        Sprite spriteToUse = null;
        switch (type)
        {
            case ItemType.XPPickup: spriteToUse = xpSprite; break;
            case ItemType.ShieldPickup: spriteToUse = shieldSprite; break;
            case ItemType.HealthPickup: spriteToUse = healthSprite; break;
    
        }

        itemSpriteRenderer.sprite = spriteToUse;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"Player picked up {type}");
            bool effectApplied = ApplyEffect(other.gameObject);
                 ReleaseToPool();
        }
    }

    private bool ApplyEffect(GameObject playerObject)
    {
        switch (type)
        {
            case ItemType.XPPickup:
                PlayerXP playerXP = playerObject.GetComponent<PlayerXP>();
                if (playerXP != null)
                {
                    playerXP.AddXP(xpAmount);
                    return true;
                }
                Debug.LogWarning("Player tried to pick up XP but has no PlayerXP component!", playerObject);
                return false;

            case ItemType.ShieldPickup:
                PlayerShield playerShield = playerObject.GetComponent<PlayerShield>();
                if (playerShield != null)
                {
                    playerShield.ActivateShield(shieldDuration);
                    return true;
                }
                Debug.LogWarning("Player tried to pick up Shield but has no PlayerShield component!", playerObject);
                return false;

            case ItemType.HealthPickup:
                Health health = playerObject.GetComponent<Health>();
                if(health != null){
                    health.HealthPickup(healthAmount);
                    return true;
                }
                Debug.Log("Health Pickup Failed",playerObject);
                return false; 


            default:
                Debug.LogWarning($"Unhandled item type: {type}");
                return false;
        }
    }

    private void ReleaseToPool()
    {
        if (_releaseAction != null)
        {
            _releaseAction.Invoke();
        }
        else
        {
             Debug.LogWarning($"Item {gameObject.name} tried to release but has no release action. Destroying instead.");
             Destroy(gameObject);
        }
    }
    void OnValidate()
    {
        if (itemSpriteRenderer == null)
        {
            itemSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
        gameObject.name = $"Item_{type}";
        ConfigureVisuals();
    }
}
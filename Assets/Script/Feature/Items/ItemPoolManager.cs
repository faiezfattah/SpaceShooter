
using UnityEngine;
using System.Collections.Generic; 

public class ItemPoolManager : MonoBehaviour
{
    [Header("Item Pools")]
    [SerializeField] private ItemPool xpPool;
    [SerializeField] private ItemPool shieldPool;
    [SerializeField] private ItemPool healthPool;

    public static ItemPoolManager Instance { get; private set; }

    void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Another ItemPoolManager instance found! Destroying this one.", gameObject);
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        if (xpPool == null || shieldPool == null || healthPool == null ) {
            Debug.LogError("One or more item pools are not assigned in the ItemPoolManager Inspector!", this);
        }
    }


    // Call this from other scripts (like enemy death) to spawn an item
    // Example: ItemPoolManager.Instance.SpawnItem(Item.ItemType.XP, enemyPosition);
    public void SpawnItem(Item.ItemType type, Vector3 position)
    {
        ItemPool poolToUse = null;

        switch (type)
        {
            case Item.ItemType.XPPickup:
                poolToUse = xpPool;
                break;
            case Item.ItemType.ShieldPickup:
                poolToUse = shieldPool;
                break;
            case Item.ItemType.HealthPickup:
                poolToUse = healthPool;
                break;
            default:
                Debug.LogError($"No pool configured or found for item type: {type}", this);
                break;
        }

        if (poolToUse != null)
        {
            poolToUse.Spawn(position);
        }
        else
        {
            Debug.LogError($"Could not spawn item of type {type} at {position} - Pool is missing or not assigned.", this);
        }
    }



}
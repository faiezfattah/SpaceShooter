
using UnityEngine;
using System.Collections.Generic; 

public enum ItemType
{
    XPPickup,
    ShieldPickup,
    HealthPickup,
    PatternUpgradeSingle,
    PatternUpgradeCone
}


public class ItemPoolManager : MonoBehaviour
{
    [Header("Item Pools")]
    [SerializeField] private ItemPool xpPool;
    [SerializeField] private ItemPool shieldPool;
    [SerializeField] private ItemPool healthPool;

    public static ItemPoolManager Instance { get; private set; }

    void Start()
    {
        Instance = this;
    }


public void SpawnItem(ItemType type, Vector3 position)
{
    ItemPool poolToUse = null;

    switch (type)
    {
        case ItemType.XPPickup:        
            poolToUse = xpPool;
            break;
        case ItemType.ShieldPickup:    
            poolToUse = shieldPool;
            break;
        case ItemType.HealthPickup:    
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
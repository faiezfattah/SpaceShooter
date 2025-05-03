using UnityEngine;

[CreateAssetMenu(fileName = "NewShieldItemData", menuName = "Item Data/Shield Pickup")]
public class ShieldItemData : ItemData
{
    [Header("Shield Specific")]
    public float shieldDuration = 5f;

    public override bool ApplyEffect(GameObject target)
    {
        PlayerShield playerShield = target.GetComponent<PlayerShield>();
        playerShield.ActivateShield(shieldDuration);
        Debug.Log($"Activated Shield: {shieldDuration}s from {itemName}");
         return true;
    }
}
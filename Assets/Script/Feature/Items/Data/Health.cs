// Filename: HealthItemData.cs
using UnityEngine;

[CreateAssetMenu(fileName = "NewHealthItemData", menuName = "Item Data/Health Pickup")]
public class HealthItemData : ItemData
{
    [Header("Health Specific")]
    public int healthAmount = 1;

    public override void ApplyEffect(GameObject target)
    {
        Debug.Log($"Health Pickup ({itemName}, {healthAmount} HP) - PlayerHealth script needed on {target.name}.");
    }
}
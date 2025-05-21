
using UnityEngine;

[CreateAssetMenu(fileName = "NewBulletPatternItemData", menuName = "Item Data/Bullet Pattern Pickup")]
public class BulletPatternItemData : ItemData
{
    [Header("Pattern Specific")]
    [Tooltip("The BulletPattern ScriptableObject asset this item will equip to the player.")]
    public BulletPattern patternToEquip;

    public override void ApplyEffect(GameObject targetPlayer)
    {
        if (patternToEquip == null)
        {
            Debug.LogError($"Item '{itemName}' (BulletPatternItemData) has no 'patternToEquip' assigned!", this);

        }

        PlayerShooting playerShooting = targetPlayer.GetComponent<PlayerShooting>();
        if (playerShooting != null)
        {
            //playerShooting.SetPattern(patternToEquip);
            Debug.Log($"Player equipped new bullet pattern: '{patternToEquip.name}' from item '{itemName}'.");
        }
        else
        {
            Debug.LogWarning($"Player '{targetPlayer.name}' picked up item '{itemName}' but has no PlayerShooting component.", targetPlayer);
        }
    }
}
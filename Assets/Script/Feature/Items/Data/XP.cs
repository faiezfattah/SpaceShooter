
using UnityEngine;

[CreateAssetMenu(fileName = "NewXPItemData", menuName = "Item Data/XP Pickup")]
public class XPItemData : ItemData 
{
    [Header("XP Specific")]
    public int xpAmount = 10;

    public override bool ApplyEffect(GameObject target)
    {
        PlayerXP playerXP = target.GetComponent<PlayerXP>();
        playerXP.AddXP(xpAmount);
        Debug.Log($"Applied XP: {xpAmount} from {itemName}");
        return true;
    }
}

using UnityEngine;

public abstract class ItemData : ScriptableObject
{
    [Header("Common Item Info")]
    public string itemName = "New Item";
    [TextArea]
    public string description = "Item Description";
    public Sprite icon; 

    public abstract bool ApplyEffect(GameObject target);
}
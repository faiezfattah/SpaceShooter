
using UnityEngine;

public abstract class ItemData : ScriptableObject
{
    [Header("Common Item Info")]
    public string itemName = "New Item";
    [TextArea]
    public string description = "Item Description";
    public Sprite icon; 

    [Header("Pickup Behavior")]
    public bool requiresUIPrompt = false; // Default to false (instant pickup)

    public abstract void ApplyEffect(GameObject target);
}
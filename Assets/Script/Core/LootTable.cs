using System;
using UnityEngine;

[Serializable]
public class LootTable {
    public ItemData Item;
    public MinMax Value;
    [Range(0, 1)]
    public float Chance;
}
[Serializable]
public class TargetedLootTable {
    public Transform Target;
    public LootTable lootTable;
}
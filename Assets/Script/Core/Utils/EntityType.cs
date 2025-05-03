using System;
using UnityEngine;

public enum EntityType {
    Player,
    Enemy,
    All
}
public static class EntityTypeUtils {
    
    // life love laugh pattern matching
    // imagine i made a monad in this project lmao
    public static LayerMask GetDamagingMask(this EntityType type) => type switch {
        EntityType.Enemy => LayerMask.NameToLayer("DamagingEnemy"),
        EntityType.Player => LayerMask.NameToLayer("DamagingPlayer"),
        EntityType.All => LayerMask.NameToLayer("Default"),
        _ => throw new ArgumentOutOfRangeException("Enity type not supported"),
    };
    public static LayerMask GetMask(this EntityType type) => type switch {
        EntityType.Enemy => LayerMask.NameToLayer("Enemy"),
        EntityType.Player => LayerMask.NameToLayer("Player"),
        EntityType.All => LayerMask.NameToLayer("Default"),
        _ => throw new ArgumentOutOfRangeException("Enity type not supported"),
    };
} 
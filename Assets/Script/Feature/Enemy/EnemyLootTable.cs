using System;
using System.Collections.Generic;
using TriInspector;
using UnityEngine;

public class EnemyLootTable : MonoBehaviour
{
    [TableList, ShowInInspector, SerializeField]
    List<LootTable> lootTable;
    [TableList, ShowInInspector, SerializeField]
    List<TargetedLootTable> targetedLootTable;
    IDisposable _subs;
    void Start() {
        _subs = EnemyHealth.EnemyKilled.Subscribe(HandleEnemyKilled);
    }
    void HandleEnemyKilled(Transform transform) {
        var target = targetedLootTable.Find(item => item.Target == transform);
        // if there is a targeted loot table we use that instead of the normal loot
        if (target is not null) {
            DropLoot(target.Target, target.lootTable);
            return;
        }
        // normal loot
        var rand = UnityEngine.Random.Range(0, lootTable.Count);
        var loot = lootTable[rand];
        DropLoot(transform, loot);
    }
    void DropLoot(Transform position, LootTable loot) {
        bool isDrop = UnityEngine.Random.Range(0f, 1f) > loot.Chance;
        
        if (isDrop) {
            var count = loot.Value.GetRandom();
            // todo: call item here
        }
    }
    void OnDisable() {
        _subs.Dispose();
    }
}

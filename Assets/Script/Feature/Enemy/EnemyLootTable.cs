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
    [SerializeField] ItemPool itemPool;
    IDisposable _subs;
    void Start() {
        _subs = EnemyHealth.EnemyKilled.Subscribe(HandleEnemyKilled);
    }
    void HandleEnemyKilled(Transform transform) {
        var target = targetedLootTable.Find(item => item.Target == transform);
        var pos = transform.position;
        // if there is a targeted loot table we use that instead of the normal loot
        if (target is not null)
        {
            DropLoot(pos, target.lootTable);
            return;
        }

        // normal loot
        if (lootTable.Count == 0) return;
        var rand = UnityEngine.Random.Range(0, lootTable.Count);
        var loot = lootTable[rand];
        DropLoot(pos, loot);
    }
    void DropLoot(Vector3 position, LootTable loot) {
        bool isDrop = UnityEngine.Random.Range(0f, 1f) < loot.Chance;

        if (isDrop)
        {
            var count = loot.Value.GetRandom();
            // todo: call item here
            itemPool.Spawn(position, loot.Item);
        }
    }
    void OnDisable() {
        _subs.Dispose();
    }
}

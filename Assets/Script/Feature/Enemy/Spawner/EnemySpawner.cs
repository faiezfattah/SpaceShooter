using System.Threading.Tasks;
using Script.Feature.Bullet;
using Unity.Behavior;
using UnityEngine;
using System.Linq;
using Script.Core.Events;
using System;
public class EnemySpawner : MonoBehaviour {
    [SerializeField] BulletPool bulletPool;
    [SerializeField] BehaviorGraphAgent enemyPrefab;
    [SerializeField] MinMaxFloat spawningInterval;
    [SerializeField] MinMax enemiesToSpawn;
    IDisposable subs;

    Transform[] spawnpoints;
    void Start() {
        spawnpoints = gameObject.GetComponentsInChildren<Transform>();
        enemyPrefab.BlackboardReference.SetVariableValue("BulletPool", bulletPool);

        subs = EventBus.Subscribe<TEST_BulletSpawn>(() => Debug.Log("Detected bullet event"));

        _ = SpawningTask();
    }

    async Task SpawningTask() {
        int enemiesLeft = enemiesToSpawn.GetRandom();
        while (enemiesLeft > 0) {
            Instantiate(enemyPrefab, GetRanomSpawnPoints().position, Quaternion.identity, transform);
            await Task.Delay((int)(spawningInterval.GetRandom() * 1000));
            enemiesLeft--;
        }
        return;
    }
    Transform GetRanomSpawnPoints() {
        int index = UnityEngine.Random.Range(1, spawnpoints.Count());
        return spawnpoints[index];
    }
    void OnDestroy() {
        subs.Dispose();
    }
}

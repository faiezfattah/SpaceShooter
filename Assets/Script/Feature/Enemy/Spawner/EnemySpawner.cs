using System.Threading.Tasks;
using Script.Feature.Bullet;
using Unity.Behavior;
using UnityEngine;
using System.Linq;
public class EnemySpawner : MonoBehaviour {
    [SerializeField] BulletPool bulletPool;
    [SerializeField] BehaviorGraphAgent enemyPrefab;
    [SerializeField] MinMaxFloat spawningInterval;
    [SerializeField] MinMax enemiesToSpawn;
    Transform[] spawnpoints;
    private static ReactiveSubject _enemySpawned = new();
    public static IReactive EnemySpawned => _enemySpawned;
    void Start() {
        spawnpoints = gameObject.GetComponentsInChildren<Transform>();
        enemyPrefab.BlackboardReference.SetVariableValue("BulletPool", bulletPool);

        _ = SpawningTask();
    }

    async Task SpawningTask() {
        int enemiesLeft = enemiesToSpawn.GetRandom();
        while (enemiesLeft > 0) {
            Instantiate(enemyPrefab, GetRanomSpawnPoints().position, Quaternion.identity, transform);
            _enemySpawned.Raise();
            await Task.Delay((int)(spawningInterval.GetRandom() * 1000));
            enemiesLeft--;
        }
        return;
    }
    Transform GetRanomSpawnPoints() {
        int index = UnityEngine.Random.Range(1, spawnpoints.Count());
        return spawnpoints[index];
    }
}

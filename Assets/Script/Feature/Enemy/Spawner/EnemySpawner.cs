using System.Threading.Tasks;
using Script.Feature.Bullet;
using Unity.Behavior;
using UnityEngine;
using System.Linq;
public class EnemySpawner : MonoBehaviour {
    [SerializeField] BulletPool bulletPool;
    [SerializeField] GameObject player;
    [SerializeField] BlackboardReference sharedBlackboard;
    [SerializeField] BehaviorGraphAgent enemyPrefab;
    [SerializeField] MinMaxFloat spawningInterval;
    [SerializeField] MinMax enemiesToSpawn;
    Transform[] _spawnpoints;
    public static IReactive EnemySpawned => _enemySpawned;
    private static ReactiveSubject _enemySpawned = new();
    void Start() {
        _spawnpoints = gameObject.GetComponentsInChildren<Transform>();

        sharedBlackboard.SetVariableValue("BulletPool", bulletPool);
        sharedBlackboard.SetVariableValue("Player", player);

        sharedBlackboard.GetVariableValue("Player", out GameObject p);
        Debug.Log("player: " + p);
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
        int index = UnityEngine.Random.Range(1, _spawnpoints.Count());
        return _spawnpoints[index];
    }
}

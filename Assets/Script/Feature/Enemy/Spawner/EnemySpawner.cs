using System.Threading.Tasks;
using Script.Feature.Bullet;
using Unity.Behavior;
using UnityEngine;
using TriInspector;
using System;
using System.Linq;
public class EnemySpawner : MonoBehaviour {
    [SerializeField] BulletPool bulletPool;
    [SerializeField] BehaviorGraphAgent enemyPrefab;
    [SerializeField] MinMaxFloat spawningInterval;
    [SerializeField] MinMax enemiesToSpawn;


    Transform[] spawnpoints;
    void Start() {
        spawnpoints = gameObject.GetComponentsInChildren<Transform>();
        enemyPrefab.BlackboardReference.SetVariableValue("BulletPool", bulletPool);


        // Instantiate(enemyPrefab, transform.position, Quaternion.identity);

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


    [Serializable]
    class MinMax {
        public int Min;
        public int Max;
        public int GetRandom() {
            return UnityEngine.Random.Range(Min, Max);
        }
    }
    [Serializable]
    class MinMaxFloat {
        public float Min;
        public float Max;
        public float GetRandom() {
            return UnityEngine.Random.Range(Min, Max);
        }
    }
}

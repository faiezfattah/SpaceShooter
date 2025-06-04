using UnityEngine;
using TriInspector;
using System;
public class EnemyStage : MonoBehaviour {
    [SerializeField, TableList] IndexedTransform[] stage;
    int _currentStage = 0;
    int _currentStageChildCount;
    int _currentChildCount;
    IDisposable _subscription;

    public static ReactiveSubject OnAllEnemiesCleared = new();
    public static ReactiveSubject OnStageCleared = new();

    void Start() {
        stage[_currentStage].transform.gameObject.SetActive(true);
        _currentStageChildCount = stage[_currentStage].transform.childCount;

        _subscription = EnemyHealth.EnemyKilled.Subscribe(enemy => HandleEnemyDeath(enemy));
    }
    void HandleEnemyDeath(Transform enemy) {
        if (enemy.parent == stage[_currentStage].transform) {
            _currentChildCount++;
            if (_currentChildCount >= _currentStageChildCount) {
                OnStageCleared.Raise();
                NextStage();
            }
        }
    }
    void NextStage() {
        _currentStage++;
        _currentChildCount = 0;;
        if (_currentStage == stage.Length) {
            Debug.Log("enemies cleard");
            OnAllEnemiesCleared.Raise();
            return;
        }

        stage[_currentStage].transform.gameObject.SetActive(true);
        _currentStageChildCount = stage[_currentStage].transform.childCount;
    }
    void OnDisable() {
        _subscription?.Dispose();
    }
}
[Serializable]
public class IndexedTransform {
    public int index;
    public Transform transform;
}
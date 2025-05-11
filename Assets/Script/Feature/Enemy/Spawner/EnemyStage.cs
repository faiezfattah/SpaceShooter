using UnityEngine;
using TriInspector;
using System;
public class EnemyStage : MonoBehaviour {
    [SerializeField, TableList] IndexedTransform[] stage;
    int _currentStage = 0;
    int _currentStageChildCount;
    int _currentChildCount;
    IDisposable _subscription;
    public static ReactiveSubject AllEnemiesCleared = new();
    public static ReactiveSubject StageCleared = new();
    void Start() {
        stage[_currentStage].transform.gameObject.SetActive(true);
        _currentStageChildCount = stage[_currentStage].transform.childCount;

        _subscription = EnemyHealth.EnemyKilled.Subscribe(HandleEnemyDeath);
    }
    void HandleEnemyDeath() {
        _currentChildCount++;
        
        if (_currentChildCount == _currentStageChildCount) {
            StageCleared.Raise();
            NextStage();
        }
    }
    void NextStage() {
        _currentStage++;
        _currentChildCount = 0;
        
        if (_currentStage == stage.Length) {
            Debug.Log("enemies cleard");
            AllEnemiesCleared.Raise();
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
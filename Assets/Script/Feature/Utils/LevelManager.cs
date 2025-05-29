using System;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelManager : MonoBehaviour {
    [SerializeField] string nextLevel;
    [SerializeField] UIDocument ui;
    IDisposable _subs;
    readonly SceneListNavigation _sceneNav = new();
    void Start() {
        _subs = EnemyStage.OnAllEnemiesCleared.Subscribe(HandleClear);
    }
    void HandleClear() {
        _sceneNav.Init();
        ui.gameObject.SetActive(true);
        ui.rootVisualElement.Q<Button>().clicked += HandleNextLevel;
    }
    async void HandleNextLevel() {
        if (string.IsNullOrEmpty(nextLevel)) {
            Debug.LogError("Next level has not been specified");
        }
        await _sceneNav.LoadLevel(nextLevel);
    }
    void OnDisable() {
        _subs.Dispose();
    }
}
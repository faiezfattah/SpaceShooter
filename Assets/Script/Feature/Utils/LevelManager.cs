using System;
using System.Threading.Tasks;
using TriInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LevelManager : MonoBehaviour {
    [SerializeField] AssetReference nextLevel;
    [SerializeField] UIDocument ui;
    IDisposable _subs;
    readonly SceneListNavigation _sceneNav = new();
    [SerializeField] bool gotoMainMenu;
    void Start() {
        _subs = EnemyStage.OnAllEnemiesCleared.Subscribe(HandleClear);
    }
    async void HandleClear() {
        
        await _sceneNav.Init();
        ui.gameObject.SetActive(true);
        ui.rootVisualElement.Q<Button>().clicked += HandleNextLevel;
    }
    async void HandleNextLevel() {
        // if (string.IsNullOrEmpty(nextLevel)) {
        //     Debug.LogError("Next level has not been specified");
        //     return;
        // }
        if (gotoMainMenu)
        {
            SceneManager.LoadScene("MENU"); 
            return;
        }
        await _sceneNav.LoadLevel(nextLevel);
     }
    void OnDisable() {
        _subs.Dispose();
    }
}
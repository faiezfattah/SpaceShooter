using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class SceneNavigation {
    public string LevelLabel = "Levels"; 
    public List<string> LevelKeys = new();
    public string CurrentLevelKey => _currentLevelKey;
    string _currentLevelKey = "";
    public static ReactiveSubject SceneChange = new();
    async void Start() {
        await LoadLevelKeys();
        DisplayLevelKeys();
    }

    async Task LoadLevelKeys() {   
        var taskHandle = Addressables.LoadResourceLocationsAsync(LevelLabel);
        await taskHandle.Task;

        if (taskHandle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded) {
            
            foreach (var location in taskHandle.Result) {
                if (location != null && !string.IsNullOrEmpty(location.PrimaryKey)) {
                    LevelKeys.Add(location.PrimaryKey);
                }
            }
        }
        else {
            Debug.LogError($"Failed to load resource locations for label: {LevelLabel}");
        }

        Addressables.Release(taskHandle);
    }

    void DisplayLevelKeys() {
        Debug.Log("Available Level Keys:");
        foreach (string key in LevelKeys) {
            Debug.Log(key);
        }
    }

    public void LoadLevel(string levelKey) {
        if (_currentLevelKey == levelKey) return;
        _currentLevelKey = levelKey;
        
        if (LevelKeys.Contains(levelKey)) {
            Addressables.LoadSceneAsync(levelKey);
            SceneChange.Raise();
        }
        else {
            Debug.LogError($"Level key not found: {levelKey}");
        }
    }
}
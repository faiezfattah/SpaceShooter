using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class SceneNavigation {
    public string levelLabel = "Levels"; 
    public List<string> LevelKeys = new();

    async void Start() {
        await LoadLevelKeys();
        DisplayLevelKeys();
    }

    async Task LoadLevelKeys() {   
        var taskHandle = Addressables.LoadResourceLocationsAsync(levelLabel);
        await taskHandle.Task;

        if (taskHandle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded) {
            
            foreach (var location in taskHandle.Result) {
                if (location != null && !string.IsNullOrEmpty(location.PrimaryKey)) {
                    LevelKeys.Add(location.PrimaryKey);
                    Debug.Log(location.PrimaryKey);
                }
            }
        }
        else {
            Debug.LogError($"Failed to load resource locations for label: {levelLabel}");
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
        if (LevelKeys.Contains(levelKey)) {
            Addressables.LoadSceneAsync(levelKey);
        }
        else {
            Debug.LogError($"Level key not found: {levelKey}");
        }
    }
}
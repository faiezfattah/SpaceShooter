using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class SceneListNavigation {
    private string _levelLabel = "Levels"; 
    public List<string> LevelKeys = new();
    public string CurrentLevelKey => _currentLevelKey;
    string _currentLevelKey = "";
    bool _initStatus = false;
    public static ReactiveSubject<string> OnSceneChange = new();
    public async Task Init() {
        await LoadLevelKeysAsync();
        DisplayLevelKeys();
        _initStatus = true;
    }

    async Task LoadLevelKeysAsync() {   
        var taskHandle = Addressables.LoadResourceLocationsAsync(_levelLabel);
        await taskHandle.Task;

        if (taskHandle.Status == AsyncOperationStatus.Succeeded) {
            
            foreach (var location in taskHandle.Result) {
                if (location != null && !string.IsNullOrEmpty(location.PrimaryKey)) {
                    LevelKeys.Add(location.PrimaryKey);
                }
            }
        }
        else {
            Debug.LogError($"Failed to load resource locations for label: {_levelLabel}");
        }

        Addressables.Release(taskHandle);
    }

    void DisplayLevelKeys() {
        Debug.Log("Available Level Keys:");
        foreach (string key in LevelKeys) {
            Debug.Log(key);
        }
    }

    public async Task LoadLevel(string levelKey) {
        if (!_initStatus) await LoadLevelKeysAsync();
        if (_currentLevelKey == levelKey) return;

        
        if (LevelKeys.Contains(levelKey)) {
            Addressables.LoadSceneAsync(levelKey);

            _currentLevelKey = levelKey;
            OnSceneChange.Raise(levelKey);
        }
        else {
            Debug.LogError($"Level key not found: {levelKey}");
        }
    }
}
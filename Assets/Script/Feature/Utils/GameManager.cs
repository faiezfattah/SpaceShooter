using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    SubscriptionBag _bag = new();
    private static GameManager _existingInstance;
    private static SceneListNavigation _sceneNav = new();
    [SerializeField] AssetReference level;
    void Awake() {
        DontDestroyOnLoad(this);
        if (_existingInstance == null){
            _existingInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        else if (_existingInstance != this) {
            Debug.LogWarning("[Game Manager] Duplicate instance of Game Manager.");
            Destroy(gameObject);
        }
    }

    void Start() {
        EnemyStage.OnAllEnemiesCleared.Subscribe(HandleClear).AddTo(_bag);
        PlayerHealth.PlayerDeath.Subscribe(HandleGameOver).AddTo(_bag);
    }
    void HandleClear() {

    }
    void HandleGameOver() {
        SceneManager.LoadScene("MENU"); 
    }
    void OnDisable() {
        _bag.Dispose();
    }
    void OnDestroy() {
        if (_existingInstance == this) {
            _existingInstance = null;
        }
    }
}
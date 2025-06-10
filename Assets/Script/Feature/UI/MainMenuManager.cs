using System.Threading.Tasks;
using TriInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UIElements;

public class MainMenuManager : MonoBehaviour
{
    Button _start;
    Button _exit;
    SceneListNavigation _sceneNavigation = new();
    [SerializeField] AssetReference level1;
    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        _start = root.Q<Button>("Btn_Start");
        _exit = root.Q<Button>("Btn_Exit");

        _exit.clicked += ExitGame;
        _start.clicked += StartGame;
    }
    void ExitGame()
    {
        Application.Quit();
        Debug.Log("Keluar game (Editor nggak nutup)");
    }
    void StartGame()
    {
        Debug.Log("Starting game");
        _ = StartGameInternal();
    }
    async Task StartGameInternal()
    {    
        await _sceneNavigation.LoadLevel(level1);
    }
    void OnDisable()
    {
        _exit.clicked -= ExitGame;
        _start.clicked -= StartGame;
    }
}

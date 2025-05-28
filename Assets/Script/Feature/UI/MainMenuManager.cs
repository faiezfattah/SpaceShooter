using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuManager : MonoBehaviour
{
    Button _start;
    Button _exit;
    SceneNavigation _sceneNavigation = new();
    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        _start = root.Q<Button>("Btn_Start");
        _exit = root.Q<Button>("Btn_Exit");

        _exit.clicked += ExitGame;
        _start.clicked += StartGame;
    }
    void Start()
    {
        _sceneNavigation.Init();
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
        await _sceneNavigation.LoadLevel(_sceneNavigation.LevelKeys[0]);
    }
    void OnDisable()
    {
        _exit.clicked -= ExitGame;
        _start.clicked -= StartGame;
    }
}

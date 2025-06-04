using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    
    private UIDocument uiDocument;      
    private VisualElement pauseContainer;
    private bool isPaused = false;

    private void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
    Application.runInBackground = true;
    }

    private void Update()
    {
            if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) ResumeGame();
            else PauseGame();
        }
    }

    private void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;

        var root = uiDocument.rootVisualElement;
        pauseContainer = root.Q<VisualElement>("pausecontainer");
        pauseContainer.style.display = DisplayStyle.Flex;

        var btnContinue = root.Q<Button>("Continue");
        var btnQuit = root.Q<Button>("Quit");

        btnContinue.clicked -= ResumeGame;
        btnQuit.clicked -= QuitToMainMenu;

        btnContinue.clicked += ResumeGame;
        btnQuit.clicked += QuitToMainMenu;
        Debug.Log("Pause ON");
        Debug.Log("Continue null? " + (btnContinue == null));
        Debug.Log("Quit null? " + (btnQuit == null));
    }

    private void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;

        if (pauseContainer != null)
            pauseContainer.style.display = DisplayStyle.None;
    }

    private void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MENU"); 
    }
}

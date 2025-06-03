using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private UIDocument uiDocument;      // referensi komponen UIDocument
    private VisualElement pauseContainer;
    private bool isPaused = false;

    private void Awake()
    {
        // Ambil sekali UIDocument
        uiDocument = GetComponent<UIDocument>();
    }

    private void Update()
    {
        // Tekan ESC untuk pause/resume
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

        // Query ulang dari root
        var root = uiDocument.rootVisualElement;
        pauseContainer = root.Q<VisualElement>("pausecontainer");
        pauseContainer.style.display = DisplayStyle.Flex;

        // Query tombol tiap pause muncul
        var btnContinue = root.Q<Button>("Continue");
        var btnQuit     = root.Q<Button>("Quit");

        // Lepas dulu agar tidak tumpuk handler
        btnContinue.clicked -= ResumeGame;
        btnQuit.clicked     -= QuitToMainMenu;

        // Daftarkan kembali event‐nya
        btnContinue.clicked += ResumeGame;
        btnQuit.clicked     += QuitToMainMenu;
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
        SceneManager.LoadScene("MainMenu"); // Ganti dengan nama scene menu utama
    }
}

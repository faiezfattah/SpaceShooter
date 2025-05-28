using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        Button Btn_Start = root.Q<Button>("Btn_Start");
        Button Btn_Exit = root.Q<Button>("Btn_Exit");

        Btn_Exit.clicked += ExitGame;
    }

    void ExitGame()
    {
        Application.Quit();
        Debug.Log("Keluar game (Editor nggak nutup)");
    }
}

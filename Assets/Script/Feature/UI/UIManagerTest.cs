using UnityEngine;
using UnityEngine.UIElements;
using System;

public class UIManagerTest : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;

    private VisualElement pickupUI;
    private Button acceptButton;
    private Button declineButton;

    private Action onAccept; 

    void Start()
    {
        var root = uiDocument.rootVisualElement;
        pickupUI = root.Q<VisualElement>("Pickup");
        acceptButton = pickupUI.Q<Button>("AcceptButton");
        declineButton = pickupUI.Q<Button>("DeclineButton");

        acceptButton.clicked += () =>
        {
            Debug.Log("Accepted!");
            onAccept?.Invoke(); // panggil fungsi ApplyEffect dll.
            pickupUI.style.display = DisplayStyle.None;
        };

        declineButton.clicked += () =>
        {
            Debug.Log("Declined!");
            pickupUI.style.display = DisplayStyle.None;
        };

        pickupUI.style.display = DisplayStyle.None; // start hidden
    }

    public void ShowPrompt(ItemData item, Action onAcceptAction)
    {
          Debug.Log($"ShowPrompt called with item: {item.itemName}");
        onAccept = onAcceptAction;
        pickupUI.style.display = DisplayStyle.Flex;
    }
}

using UnityEngine;
using UnityEngine.UIElements;
using System;
using System.Collections; // IEnumerator & Coroutine
using Script.Core.Pool;
using Unity.VisualScripting;
public class UIManager : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;

    //ui di ujung (main)
    private VisualElement healthBar;
    private Label bulletTypeLabel;
    private VisualElement xpBar;
    private Label levelLabel;

//ui choice pickup2
    private VisualElement pickupUI;
    private Button acceptButton;
    private Button declineButton;
   
    private Action onAccept;
    SubscriptionBag _bag = new();

    void Start()
    {
        StartCoroutine(InitUI());
        PlayerShooting.OnBulletPatternChanged.Subscribe(x => UpdateBulletType(x.name)).AddTo(_bag);
        SceneListNavigation.OnSceneChange.Subscribe(x => UpdateLevel(x)).AddTo(_bag);
    }

    private IEnumerator InitUI()
    {
        yield return new WaitForSeconds(0.5f);

        var root = GetComponent<UIDocument>().rootVisualElement;
        if (root == null)
        {
            Debug.LogError("UI root not found!");
            yield break;
        }

        healthBar = root.Q<VisualElement>("HealthBar");
        bulletTypeLabel = root.Q<Label>("BulletLabel");
        xpBar = root.Q<VisualElement>("XPBar");
        levelLabel = root.Q<Label>("LevelLabel");

        // ambil elemen 
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


        Debug.Log("UI ELEMENTS CONNECTED!");

        PlayerHealth.CurrentPlayerHealth.Subscribe(OnHealthChanged).AddTo(_bag);
    }

    public void ShowPrompt(ItemData item, Action onAcceptAction)
    {
        Debug.Log($"ShowPrompt called with item: {item.itemName}");
        onAccept = onAcceptAction;
        pickupUI.style.display = DisplayStyle.Flex;
    }

    void OnDisable()
    {
        _bag?.Dispose();
    }

    private void OnHealthChanged(int newHealth) {
        // Debug.Log("HEALTH UPDATED: " + newHealth);
        float percent = (float)newHealth / 5;
        UpdateHealth(percent);
    }

    public void UpdateHealth(float percent)
    {
        // Debug.Log("HEALTH UPDATED: " + percent);
        healthBar.style.width = new Length(400 * percent, LengthUnit.Pixel);
    }

    public void UpdateBulletType(string typeName)
    {
        bulletTypeLabel.text = "Bullet Type: " + typeName;
    }

    public void UpdateXP(float percent)
    {
        xpBar.style.width = new Length(200 * percent, LengthUnit.Pixel);
    }

    public void UpdateLevel(string level)
    {
        levelLabel.text = level;
    }

}

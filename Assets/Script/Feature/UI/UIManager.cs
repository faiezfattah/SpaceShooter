using UnityEngine;
using UnityEngine.UIElements;
using System.Collections; // IEnumerator & Coroutine


public class UIManager : MonoBehaviour
{
    private VisualElement healthBar;
    private Label bulletTypeLabel;
    private VisualElement xpBar;
    private Label levelLabel;

   void Start()
{
    StartCoroutine(InitUI());
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
    bulletTypeLabel = root.Q<Label>("BulletTypeLabel");
    xpBar = root.Q<VisualElement>("XPBar");
    levelLabel = root.Q<Label>("LevelLabel");

    Debug.Log("UI ELEMENTS CONNECTED!");

    if (PlayerHealth.CurrentPlayerHealth == null)
    {
        Debug.LogError("CurrentPlayerHealth is NULL!!!");
        yield break;
    }

    PlayerHealth.CurrentPlayerHealth.Subscribe(OnHealthChanged);
}


    

    private void OnEnable()
{
    Debug.Log("UIMANAGER ENABLED!");

     if (PlayerHealth.CurrentPlayerHealth == null)
    {
        Debug.LogError("CurrentPlayerHealth is NULL!!!");
        return;
    }
}

private void OnHealthChanged(int newHealth)
{
    Debug.Log("HEALTH UPDATED: " + newHealth);
    float percent = (float)newHealth / 5; 
    UpdateHealth(percent);
}


    public void UpdateHealth(float percent)
    {
        Debug.Log("HEALTH UPDATED: " + percent);
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

    public void UpdateLevel(int level)
    {
        levelLabel.text = "Level " + level.ToString();
    }
}

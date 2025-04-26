using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    private VisualElement healthBar;
    private Label bulletTypeLabel;
    private VisualElement xpBar;
    private Label levelLabel;

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        healthBar = root.Q<VisualElement>("HealthBar");
        bulletTypeLabel = root.Q<Label>("BulletTypeLabel");
        xpBar = root.Q<VisualElement>("XPBar");
        levelLabel = root.Q<Label>("LevelLabel");
    }

    public void UpdateHealth(float percent)
    {
        healthBar.style.width = new Length(200 * percent, LengthUnit.Pixel);
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

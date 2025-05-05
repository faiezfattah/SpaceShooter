using UnityEngine;
using UnityEngine.Events;

public class PlayerXP : MonoBehaviour
{
    [Header("Experience & Level")]
    [SerializeField] public static ReactiveProperty<int> currentXP = new(0);
    [SerializeField] int xpToNextLevel = 100;
    [SerializeField] public static ReactiveProperty<int> currentLevel = new(1);
    [SerializeField] float levelXpFactor = 1.5f;

    public static IReactive<int> CurrentLevel => currentLevel;
    public static IReactive<int> CurrentXP => currentXP;
    public int XPToNextLevel => xpToNextLevel;

    public void AddXP(int amount)
    {
        if (amount <= 0) return;

        currentXP.Value += amount;
        Debug.Log($"Gained {amount} XP. Total: {currentXP}/{xpToNextLevel}");

        while (currentXP.Value >= xpToNextLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        currentLevel.Value++;
        currentXP.Value -= xpToNextLevel;
        xpToNextLevel = Mathf.FloorToInt(xpToNextLevel * levelXpFactor);
    }

    public void SetXP(int level, int xp, int nextLevelXP)
    {
        currentLevel.Value = level;
        currentXP.Value = xp;
        xpToNextLevel = nextLevelXP;
    }
}

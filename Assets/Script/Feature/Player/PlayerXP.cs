using UnityEngine;
using UnityEngine.Events;

public class PlayerXP : MonoBehaviour
{
    [Header("Experience & Level")]
    [SerializeField] private int currentXP = 0;
    [SerializeField] private int xpToNextLevel = 100;
    [SerializeField] private int currentLevel = 1;
    [SerializeField] private float levelXpFactor = 1.5f; // How much harder each level gets

    // This event will fire when the player levels up. Other scripts can listen to this!
    public UnityEvent<int> OnLevelUp; // Sends the new level number

    // Public properties to safely access stats from other scripts if needed
    public int CurrentLevel => currentLevel;
    public int CurrentXP => currentXP;
    public int XPToNextLevel => xpToNextLevel;

    // Call this method when the player should gain XP
    public void AddXP(int amount)
    {
        if (amount <= 0) return; // Don't add negative XP

        currentXP += amount;
        Debug.Log($"Gained {amount} XP. Total: {currentXP}/{xpToNextLevel}");

        // Check for level up
        while (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }
        // Maybe add an event here for UI updates about XP gain?
    }

    private void LevelUp()
    {
        currentLevel++;
        currentXP -= xpToNextLevel; // Subtract the cost of the level up

        // Make the next level require more XP
        xpToNextLevel = Mathf.FloorToInt(xpToNextLevel * levelXpFactor);

        Debug.Log($"LEVEL UP! Reached Level {currentLevel}. Next level in {xpToNextLevel} XP.");

        // Fire the level up event!
        OnLevelUp?.Invoke(currentLevel);
    }

    // Optional: For loading game state later?
    public void SetXP(int level, int xp, int nextLevelXP)
    {
        currentLevel = level;
        currentXP = xp;
        xpToNextLevel = nextLevelXP;
    }
}
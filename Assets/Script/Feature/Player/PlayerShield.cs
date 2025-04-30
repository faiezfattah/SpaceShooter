using UnityEngine;
using System.Collections;

public class PlayerShield : MonoBehaviour
{
    [Header("Shield Settings")]
    [SerializeField] private float defaultShieldDuration = 5f;
    [SerializeField] private GameObject shieldVisual; // Assign a child GameObject prefab/sprite in the inspector

    private bool isShieldActive = false;
    private Coroutine shieldTimerCoroutine;

    public bool IsShieldActive => isShieldActive; // Public getter

    void Start()
    {
        // Make sure the visual is off at the start
        if (shieldVisual != null)
        {
            shieldVisual.SetActive(false);
        }
    }

    // Call this to try and activate the shield
    public void ActivateShield(float duration = -1f) // Optional duration override
    {
        if (isShieldActive)
        {
            Debug.Log("Shield already active, maybe refresh duration?");
            // Optional: Stop existing timer and start a new one if you want pickups to refresh duration
            if (shieldTimerCoroutine != null)
            {
                StopCoroutine(shieldTimerCoroutine);
            }
        }
        else
        {
            Debug.Log("Activating shield!");
            isShieldActive = true;
            if (shieldVisual != null)
            {
                shieldVisual.SetActive(true);
            }
        }

        float actualDuration = (duration > 0) ? duration : defaultShieldDuration;
        shieldTimerCoroutine = StartCoroutine(ShieldTimer(actualDuration));
    }

    private IEnumerator ShieldTimer(float duration)
    {
        yield return new WaitForSeconds(duration);
        DeactivateShield();
    }

    public void DeactivateShield()
    {
        Debug.Log("Deactivating shield.");
        isShieldActive = false;
        if (shieldVisual != null)
        {
            shieldVisual.SetActive(false);
        }
        shieldTimerCoroutine = null; // Clear the coroutine reference
        // Maybe add cooldown logic here if needed in the future?
    }

    // Optional: Add damage handling here later
    // public void TakeDamage(int amount) {
    //     if (isShieldActive) {
    //         Debug.Log("Damage absorbed by shield!");
    //         // Maybe deactivate shield after one hit? Or give it health?
    //         // DeactivateShield();
    //         return;
    //     }
    //     // Handle player taking damage normally...
    // }
}
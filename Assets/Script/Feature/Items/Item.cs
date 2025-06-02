using UnityEngine;
using System;
using Script.Core.Pool;

public class Item : MonoBehaviour, IPoolable
{
    [SerializeField] private ItemData itemData;
    [SerializeField] private SpriteRenderer itemSpriteRenderer;

    private UIManager _uiManagerInstance;
    private Action _releaseAction; 

    public void Setup(Action releaseAction)
    {
        _releaseAction = releaseAction;
    }

    public void Reset()
    {
        _releaseAction = null;
        itemData = null;
    }

    // --- Item Logic ---
    void Start()
    {
        _uiManagerInstance = FindAnyObjectByType<UIManager>();
        if (_uiManagerInstance == null)
        {
            Debug.LogWarning("UIManager instance not found in Awake! UI prompts might not work.", this);
        }

        if (itemSpriteRenderer == null)
        {
            itemSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
            if (itemSpriteRenderer == null)
                Debug.LogError($"Item {gameObject.name} is missing a SpriteRenderer!", this);
        }
    }

    public void ApplyData(ItemData itemData)
    {
        this.itemData = itemData;
        if (itemData == null)
        {
            Debug.LogError($"Item {gameObject.name} is missing its ItemData asset!", this);
            gameObject.name = "Item_NO_DATA";
            if (itemSpriteRenderer != null) itemSpriteRenderer.sprite = null; // Clear sprite
            return;
        }

        gameObject.name = $"Item_{itemData.itemName}";

        if (itemSpriteRenderer != null)
        {
            itemSpriteRenderer.sprite = itemData.icon;
        }
        else
        {
             Debug.LogWarning($"Item {gameObject.name} has ItemData but no SpriteRenderer to display icon {itemData.icon?.name}.", this);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
     if (itemData == null)
        {
            Debug.LogWarning($"Item '{gameObject.name}' collided but has no ItemData. Aborting pickup.", this);
            return;
        }

        if (other.CompareTag("Player"))
        {
            if (itemData.requiresUIPrompt)
            {
                if (_uiManagerInstance != null)
                {
                        Debug.Log($"Item '{itemData.itemName}' requires UI prompt. Showing prompt.");
                        _uiManagerInstance.ShowPrompt(itemData, () => {

                        itemData.ApplyEffect(other.gameObject);
                        ReleaseToPool();
                    });
                }
                else
                {
                    Debug.LogError($"UIManager not found! Cannot show UI prompt for '{itemData.itemName}'. Item not picked up.", this);
                }
            }
            else
            {
                Debug.Log($"Item '{itemData.itemName}' is an instant pickup.");
                itemData.ApplyEffect(other.gameObject);
                ReleaseToPool();
            }
        }
    }

    private void ReleaseToPool()
    {
        _releaseAction?.Invoke();
    }
}
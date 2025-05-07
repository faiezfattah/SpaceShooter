using UnityEngine;
using System;
using Script.Core.Pool;

public class Item : MonoBehaviour, IPoolable
{
    [Header("Configuration")]
    [SerializeField] private ItemData itemData;

    [Header("Visuals")]
    [SerializeField] private SpriteRenderer itemSpriteRenderer;

    private Action _releaseAction; 

    public void Setup(Action releaseAction)
    {
        _releaseAction = releaseAction;
        ApplyData(); 
    }

    public void Reset()
    {
        _releaseAction = null;

    }

    // --- Item Logic ---
    void Start()
    {

        if (itemSpriteRenderer == null)
        {
            itemSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
            if(itemSpriteRenderer == null)
                Debug.LogError($"Item {gameObject.name} is missing a SpriteRenderer!", this);
        }
        ApplyData();
    }

    private void ApplyData()
    {
        if (itemData == null)
        {
            Debug.LogError($"Item {gameObject.name} is missing its ItemData asset!", this);
            gameObject.name = "Item_NO_DATA";
            if(itemSpriteRenderer != null) itemSpriteRenderer.sprite = null; // Clear sprite
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
        Debug.Log("skibidi");
        if (itemData == null) {
            Debug.LogWarning("item data is null");
        }

        if (other.CompareTag("Player")) 
        {
        Debug.Log("skibidi2");
            itemData.ApplyEffect(other.gameObject);
            ReleaseToPool();
        }
    }

    private void ReleaseToPool()
    {
        if (_releaseAction != null)
        {
            _releaseAction.Invoke();
        }
        else
        {
            Debug.LogWarning($"Item {gameObject.name} tried to release but has no release action. Destroying.", this);
            Destroy(gameObject);
        }
    }

    void OnValidate()
    {
        if (itemSpriteRenderer == null) {
             itemSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
        ApplyData();
    }
}
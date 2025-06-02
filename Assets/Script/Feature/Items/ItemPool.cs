
using UnityEngine;
using Script.Core.Pool; 

public class ItemPool : Pool<Item>
{
    public void Spawn(Vector3 position, ItemData itemData)
    {
        Item itemInstance = Get();
        itemInstance.transform.position = position;
        itemInstance.transform.rotation = Quaternion.identity;
        itemInstance.ApplyData(itemData);
        itemInstance.Setup(() => _pool.Release(itemInstance));
    }
}

using UnityEngine;
using Script.Core.Pool; 

public class ItemPool : Pool<Item>
{
    public void Spawn(Vector3 position)
    {
        Item itemInstance = Get();
        itemInstance.transform.position = position;
        itemInstance.transform.rotation = Quaternion.identity; 
    }
}
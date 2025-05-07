using UnityEngine;
using Script.Feature.Input;
using System;

public class PlayerDebug : MonoBehaviour
{

    [SerializeField] InputReader inputReader;
    [SerializeField] ItemPoolManager itemPoolManager;
    IDisposable _subscription;

    void Start() {
        _subscription = inputReader.DebugEvent.Subscribe(DebugEvent);
        
    }

    void DebugEvent(){
            itemPoolManager.SpawnItem(ItemType.XPPickup, transform.position+Vector3.up*1f);

    }



}

using UnityEngine;
using Script.Feature.Input;
using System;
using Unity.VisualScripting;

public class PlayerDebug : MonoBehaviour
{

    [SerializeField] InputReader inputReader;
    [SerializeField] ItemPool itemPool;
    IDisposable _subscription;
    [SerializeField] ItemData itemData;
    void Start() {
        _subscription = inputReader.DebugEvent.Subscribe(DebugEvent);
        
    }

    void DebugEvent (){
        itemPool.Spawn(transform.position + Vector3.up * 1f, itemData);
    }
    void OnDisable() {
        _subscription.Dispose();
    }
}

using System;
using Script.Feature.Input;
using UnityEngine;
using UnityEngine.Events;


namespace Script.Feature.Bullet {
public class TESTING_Bullet_Spawning : MonoBehaviour {
    [SerializeField] private BulletPool bulletPool;
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Transform target;
    
    ReactiveSubject _testSubject = new();
    
    // make it serializefield to see the value in the editor!
    [SerializeField] ReactiveProperty<int> testReactiveProperty = new(0);
    SubscriptionBag _bag = new();
    UnityEvent _regularUnityEvent = new();
    UnityEvent<int> _regularUnityIntEvent = new();
    void Start() {
        // this is event. plain. just like action but can be added to the bag.
        // the event can be raised whenever.
        // usefull to notify something has happened
        _testSubject.Subscribe(Test2)
                    .AddTo(_bag);
        _testSubject.Raise(); // this will raise event



        // this however is a property
        // imagine a regular int or float for health
        // if the health (int) is a reactive property
        // it fill emit events whenever health changes
        // usefull for ui
        testReactiveProperty.Subscribe(_ => Test3())
                            .AddTo(_bag);
        testReactiveProperty.Value = 1; // this will raise event


        // the sole difference between the two is how to raise events
        // reactive property is automatic (when value changes)
        // reactive subject is not.


        // when we forced to interact with unity's own actions 
        // like ui for example. otherwise we can ignore this.
        _regularUnityEvent.SubscribeUnityEvent(() => Debug.Log("hurrah2")).AddTo(_bag);
        _regularUnityIntEvent.SubscribeUnityEvent((val) => Debug.Log("hurrah4")).AddTo(_bag);

        _regularUnityEvent.Invoke();
        _regularUnityIntEvent.Invoke(1);


        // this for communication between scripts! we make reactive subject/property static 
        EnemySpawner.EnemySpawned       // ------> see EnemySpawner, 
                    .Subscribe(() => Debug.Log("enemy spawned!"))
                    .AddTo(_bag);       // use this bag for managing subscriptions so there wouldnt be 10 += and -= at OnDestroy() and OnEnable();


        inputReader.ShootingEvent.Subscribe(Test_BulletSpawning).AddTo(_bag);
    }
    void Test_BulletSpawning() {
        bulletPool.BulletRequest(transform.position, Vector2.left) // first we request where the bullet spawn and the direction
                  .WithDamage(1)                                    // add some damaga, lifetime, and speed
                  .WithLifetime(5)
                  .WithSpeed(20)
                  .WithLayerMask(LayerMask.GetMask("Player"));      // this layer mask define who shall be hit with the bullet
                                                                    // in this case its the player
    }
    void Test2() {
        Debug.Log("Subject raised correctly");
    }
    void Test3() {
        Debug.Log("Reactive property change event raised correctly");
    }
    private void OnDisable() {
        _bag.Dispose();     // just one. how simple
    }
}
}
using Script.Core.Events;
using Script.Feature.Input;
using Unity.Android.Gradle;
using UnityEngine;
namespace Script.Feature.Bullet {
public class TESTING_Bullet_Spawning : MonoBehaviour {
    [SerializeField] private BulletPool bulletPool;
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Transform target;
    
    ReactiveSubject _testSubject = new();
    // make it serializefield to see the value in the editor!
    [SerializeField] ReactiveProperty<int> testReactiveProperty = new(0);
    SubscriptionBag _bag = new();
    void Start() {
        _testSubject.Subscribe(Test2).AddTo(_bag);
        _testSubject.Raise();

        testReactiveProperty.Subscribe(Test3).AddTo(_bag);
        testReactiveProperty.Value = 1;
    }
    void Test2() {
        Debug.Log("Subject raised correctly");
    }
    void Test3() {
        Debug.Log("Reactive property change event raised correctly");
    }
    void Test() {
        bulletPool.BulletRequest(transform.position, Vector2.left);
        Debug.Log("bullet spawning!");

        EventBus.Raise<TEST_BulletSpawn>();
    }
    private void OnEnable() {
        inputReader.ShootingEvent += Test;
    }
    private void OnDisable() {
        inputReader.ShootingEvent -= Test;
        _bag.Dispose();
    }
}
}
using System;
using UnityEngine;
using UnityEngine.Pool;

namespace Script.Core.Pool {
public abstract class Pool<T> : MonoBehaviour where T : Component, IPoolable {
    [SerializeField] private T prefab;
    protected ObjectPool<T> _pool;

    protected virtual void Awake() {
        InitializePool();
    }

    protected virtual void InitializePool() {
        _pool = new ObjectPool<T>(
            CreatePooledItem,
            OnGetFromPool,
            OnReleaseToPool,
            OnDestroyPooledItem,
            false,
            10,
            1000
        );
    }

    protected virtual T CreatePooledItem() {
        return Instantiate(prefab);
    }

    protected virtual void OnGetFromPool(T item) {
        item.gameObject.SetActive(true);
    }

    protected virtual void OnReleaseToPool(T item) {
        item.Reset();
        item.gameObject.SetActive(false);
    }

    protected virtual void OnDestroyPooledItem(T item) {
        Destroy(item.gameObject);
    }

    public virtual T Get() {
        var item = _pool.Get();
        item.Setup(() => _pool.Release(item));
        return item;
    }

    public virtual void Release(T item) {
        _pool.Release(item);
    }
}
}
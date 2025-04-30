using System;
using UnityEngine;
using TriInspector;
using System.Collections.Generic;
/// <summary>
/// This class is a wrapper for values that emit events when changed. Internally this uses <see cref="ReactiveSubject"/>
/// Use it if you need to track changes within a class. Can be used with <see cref="SubscriptionBag"/>
/// </summary>
/// <typeparam name="T">The type of data. Can be int, float, vector, anything really</typeparam>
[Serializable]
public class ReactiveProperty<T> : IDisposable {
    bool _isDisposed = false;

    [SerializeField, ReadOnly]
    T _value;
    public T Value {
        get {
            if (_isDisposed) throw new ObjectDisposedException("Trying to access disposed ReactiveProperty. Cannot resume because value is discarded.");
            else return _value;
        }
        set {
            if (_isDisposed) throw new ObjectDisposedException("Trying to access disposed ReactiveProperty. Cannot resume because value is discarded.");
            if (!Equals(_value, value)) { // note: if the value is the same. doesnt trigger.
                _value = value;
                TriggerChange();
            } 
        }
    }
    ReactiveSubject _subscriber = new();
    public ReactiveProperty(T initialValue) {
        _value = initialValue;
    }
    public IDisposable Subscribe(Action action) {
        if (_isDisposed) throw new ObjectDisposedException("Trying to access disposed ReactiveProperty. Cannot resume because value is discarded.");
        return _subscriber.Subscribe(action);
    }
    void TriggerChange() {
        _subscriber?.Raise();
    }
    public void Dispose() {
        if (_isDisposed) return;
        _value = default(T);
        _subscriber.Dispose();
        _isDisposed = true;
    }
}
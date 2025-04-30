using System;
using System.Collections.Generic;
using Script.Core.Events;
using UnityEngine;

/// <summary>
/// The driver for events. Use this for cross-script communications.
/// other wise use Reactives.
/// </summary>
public static class EventBus {
    static Dictionary<Type, ReactiveSubject> _subscribers;

    [RuntimeInitializeOnLoadMethod]
    static private void Initialize() {
        _subscribers = new();
    }
    static public IDisposable Subscribe<T>(Action action) where T : IEvent {
        var eventType = typeof(T);

        if (!_subscribers.ContainsKey(eventType)) {
            _subscribers.Add(eventType, new());
        } 

        _subscribers[eventType].Subscribe(action);
        return _subscribers[eventType];
    }     
    static public void Raise<T>() where T : IEvent {
        var eventType = typeof(T);

        if (!_subscribers.ContainsKey(eventType)) { // if doesnt exist mean there is no subs we can return here
            _subscribers.Add(eventType, new());
            return;
        } 
        
        _subscribers[eventType].Raise();
    } 
}
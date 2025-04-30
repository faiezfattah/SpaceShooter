using System;
using System.Collections.Generic;
using UnityEngine;

// todo: wont work it the is scene change!
public static class ServiceLocator {

    // note: we store it here as the interface
    // thus we have to cast it back.
    public static Dictionary<Type, IService> _services = new();

    public static void Register<T>(T instance) where T : IService {
        var type = typeof(T);

        if (_services.ContainsKey(type)) {
            Debug.LogWarning($"service of type [{ type.Name }] is already registered");
            return;
        }
        _services[type] = instance;
    }
    public static void Register(Type type, IService instance) {
        if (_services.ContainsKey(type)) return;

        _services[type] = instance;
    }

    /// <summary>
    /// Only reuse this on Start() method!
    /// </summary>
    /// <typeparam name="T">The service type</typeparam>
    /// <param name="instance">The out variable for the instance</param>
    /// <exception cref="Exception">Throws an exception if no service is found</exception>
    public static void Get<T>(out T instance) where T : IService {
        var type = typeof(T);

        if (_services.TryGetValue(typeof(T), out var value)) {
            instance = (T) value;
            return;
        }
        else {
            throw new Exception("no registration for: " + type.Name);
        }
    }
    /// <summary>
    /// <inheritdoc cref="ServiceLocator.Get{T}(out T)"/>
    /// this one return the instance. 
    /// </summary>
    public static T Get<T>() where T : IService {
        var type = typeof(T);

        if (_services.TryGetValue(typeof(T), out var value)) {
            return (T) value;
        }
        else {
            throw new Exception("no registration for: " + type.Name);
        }
    }
    /// <summary>
    /// <inheritdoc cref="ServiceLocator.Get{T}(out T)"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="service"></param>
    /// <returns></returns>
    public static bool TryGet<T>(out T service) where T : IService {
        var type = typeof(T);

        if (_services.TryGetValue(type, out var serviceInstance)) {
            service = (T) serviceInstance;
            return true;
        }
        
        service = default;
        return false;
    }
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    public static void Reset() {
        _services.Clear();
    }
}
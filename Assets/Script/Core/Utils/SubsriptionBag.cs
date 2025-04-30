using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is some nice things Fafa made to manage subscription with IDiposeable interface.
/// Fafa recommends to use Subject and Reactive Property instead of Action to work with this.
/// </summary>
public class SubscriptionBag : IDisposable {
    HashSet<IDisposable> _subcription = new();
    bool _isDisposed = false;
    public static SubscriptionBag Create() {
        return new SubscriptionBag{_subcription = new()};
    }
    public void Add(IDisposable disposable) {
        if (_isDisposed) {
            Debug.LogWarning("trying to use disposed subscription bag. do not dynamically change subscription with this it may cause race condition problems. if you cant figure out how to fix your problem just contact fafa.");
            disposable.Dispose();
        }
        _subcription.Add(disposable);
    }
    public void Dispose() {
        foreach (var subs in _subcription) {
            subs.Dispose();
        }
        _subcription = null;
        _isDisposed = true;
    }
}
public static class IDiposeableExtension {
    public static void AddTo(this IDisposable disposable, SubscriptionBag bag) {
        bag.Add(disposable);
    }
}
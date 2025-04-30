using System;

/// <summary>
/// This class is a wrapper for Action to better manage local events.
/// Use this instead of Action to work with <see cref="SubscriptionBag"/>
/// Use it if you need to track changes within a class.
/// </summary>
public class ReactiveSubject : IDisposable {
    Action _subscriber;
    bool _isDisposed = false;
    public void Raise() {
            if (_isDisposed) throw new ObjectDisposedException("Trying to access disposed ReactiveSubject. Cannot resume because listener is discarded. Do no reuse this class, it may cause race conditions.");
        _subscriber?.Invoke();
    }
    public IDisposable Subscribe(Action action) {
            if (_isDisposed) throw new ObjectDisposedException("Trying to access disposed ReactiveSubject. Cannot resume because listener is discarded. Do no reuse this class, it may cause race conditions.");
        _subscriber += action;
        return this;
    }
    public void Dispose() {
        if (_isDisposed) return;
        _subscriber = null;
        _isDisposed = true;
    }
}
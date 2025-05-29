using System;

public class UnsubscribeHandle : IDisposable
{
    private Action _unsubscribeAction;
    private bool _isDisposed = false;
    
    public UnsubscribeHandle(Action unsubscribeAction) {
        _unsubscribeAction = unsubscribeAction;
    }
    
    public void Dispose() {
        if (_isDisposed) return;
        
        _unsubscribeAction?.Invoke();
        _unsubscribeAction = null;
        _isDisposed = true;
    }
}
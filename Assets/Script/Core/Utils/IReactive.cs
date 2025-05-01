using System;

/// <summary>
/// for controlling who can raise or trigger events by hiding other method behind this interface.
/// dont understand? ask fafa.
/// </summary>
public interface IReactive {
    public IDisposable Subscribe(Action action);
}
/// <summary>
/// <inheritdoc cref="IReactive"/>
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IReactive<T> {
    public IDisposable Subscribe(Action<T> action);
}
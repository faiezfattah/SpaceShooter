using System;

namespace Script.Core.Pool {
    public interface IPoolable {
        void Setup(Action releaseAction);
        void Reset();
    }
}
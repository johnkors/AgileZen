using System;

namespace Lib.Services
{
    public interface IDispatchOnUIThread
    {
        void Invoke(Action action);
    }
}
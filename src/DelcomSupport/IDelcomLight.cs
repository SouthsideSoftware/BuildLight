using System;

namespace DelcomSupport
{
    public interface IDelcomLight : IDisposable
    {
        void ChangeIndicator(DelcomIndicatorState newState);
        void Close();
    }
}
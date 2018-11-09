using System;
using System.ComponentModel;

namespace Gemini.Modules.Inspector.Inspectors
{
    public interface IInspector : INotifyPropertyChanged, IDisposable
    {
        string Name { get; }
        bool IsReadOnly { get; }
        bool CanReset { get; }
        void Reset();
    }
}

using System;
using System.Collections.Generic;
using Gemini.Modules.Inspector.Inspectors;

namespace Gemini.Modules.Inspector
{
    public interface IInspectableObject : IDisposable
    {
        IEnumerable<IInspector> Inspectors { get; }
    }
}

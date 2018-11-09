using System;
using System.Collections.Generic;
using Gemini.Modules.Inspector.Inspectors;

namespace Gemini.Modules.Inspector
{
    public class InspectableObject : IInspectableObject, IDisposable
    {
        public IEnumerable<IInspector> Inspectors { get; set; }

        public InspectableObject(IEnumerable<IInspector> inspectors)
        {
            Inspectors = inspectors;
        }

        public void Dispose()
        {
            foreach (var i in Inspectors)
                i.Dispose();
        }
    }
}

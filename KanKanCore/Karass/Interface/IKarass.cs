using System;
using System.Collections.Generic;

namespace KanKanCore.Karass.Interface
{
    public interface IKarass
    {
        void Setup();
        void Teardown();
        List<Func<string, bool>[]> Frames { get; }
    }
}
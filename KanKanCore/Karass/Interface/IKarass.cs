using System;
using System.Collections.Generic;

namespace KanKanCore.Karass.Interface
{
    public interface IKarass
    {
        void Setup();
        void Teardown();
        
        IEnumerable<Action> SetupActions { get;   set; }
        IEnumerable<Action> TeardownActions { get;  set; }
        
        List<Func<string, bool>[]> FramesCollection { get; }
    }
}
using System;
using System.Collections.Generic;

namespace KanKanCore.Karass.Interface
{
    public interface IKarass
    {
        void Setup(int index);
        void Teardown(int index);
        
        List<List<Action>>  SetupActions { get;   set; }
        List<List<Action>>  TeardownActions { get;  set; }
        
        List<Func<string, bool>[]> FramesCollection { get; }
    }
}
using System;
using System.Collections.Generic;

namespace KanKanCore.Karass.Interface
{
    public interface IKarass
    {
        void Setup(int index);
        void Teardown(int index);
        
        List<List<Action>>  SetupActions { get; }
        List<List<Action>>  TeardownActions { get; }
        
        List<Func<string, bool>[]> FramesCollection { get; }
    }
}
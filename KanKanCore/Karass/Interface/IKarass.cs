using System;
using System.Collections.Generic;
using KanKanCore.Karass.Frame;

namespace KanKanCore.Karass.Interface
{
    public interface IKarass
    {
        string ID { get; }
        void Setup(int index);
        void Teardown(int index);
        
        List<List<Action>>  SetupActions { get; }
        List<List<Action>>  TeardownActions { get; }
       
        List<FrameRequest[]> FramesCollection { get; }
    }
}
using System;
using System.Collections.Generic;
using KanKanCore.Karass.Frame;

namespace KanKanCore.Karass.Interface
{
    public interface IKarass
    {
        void Setup(int index);
        void Teardown(int index);
        
        List<List<Action>>  SetupActions { get; }
        List<List<Action>>  TeardownActions { get; }
       
        List<FrameRequest[]> FramesCollection { get; }
        
        IFrameFactory FrameFactory { get; }
        IDependencies Dependencies { get; }
    }
}
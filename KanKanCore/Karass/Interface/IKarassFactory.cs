using System;
using System.Collections.Generic;
using KanKanCore.Karass.Frame;

namespace KanKanCore.Karass.Interface
{
    public interface IKarassFactory
    {
        Karass Get(Action setup, Action teardown, FrameRequest[] frames);
        Karass Get(List<Action> setup, List<Action> teardown, FrameRequest[] frames);
        Karass Get(List<List<Action>> setup, List<List<Action>> teardown, FrameRequest[] frames);
        
        Karass Get(Action setup, Action teardown, List<FrameRequest[]>  frames);
        Karass Get(List<Action> setup, List<Action> teardown, List<FrameRequest[]>  frames);
        Karass Get(List<List<Action>> setup, List<List<Action>> teardown, List<FrameRequest[]> frames);
        
        
    }
}
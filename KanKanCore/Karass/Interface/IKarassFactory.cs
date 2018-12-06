using System;
using System.Collections.Generic;

namespace KanKanCore.Karass.Interface
{
    public interface IKarassFactory
    {
        Karass Get(Action setup, Action teardown, Func<string, bool>[] frames);
        Karass Get(List<Action> setup, List<Action> teardown, Func<string, bool>[] frames);
        Karass Get(List<List<Action>> setup, List<List<Action>> teardown, Func<string, bool>[] frames);
        
        
        Karass Get(Action setup, Action teardown, List<Func<string, bool>[]>  frames);
        Karass Get(List<Action> setup, List<Action> teardown, List<Func<string, bool>[]>  frames);
        Karass Get(List<List<Action>> setup, List<List<Action>> teardown, List<Func<string, bool>[]> frames);
        
        
    }
}
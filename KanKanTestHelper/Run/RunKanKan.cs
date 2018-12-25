using System;
using KanKanCore.Karass.Interface;
using KanKanTestHelper.Interface;
using KanKanTestHelper.Run.CurrentState;

namespace KanKanTestHelper.Run
{
    public class RunKanKan:IRunKanKan
    {
        public IRunUntil Until { get; }
        
        public RunKanKan(IKanKan kanKan, IRunUntil until)
        {
            Until = until;
            KanKan = kanKan;
        }

        public IKanKan KanKan { get; }

        public IKanKanCurrentState For(int frames)
        {
            KanKan.Reset();
            
            for (int i = 0; i < frames; i++)
            {
                KanKan.MoveNext();
            }

            return new KanKanCurrentState()
            {
                NextFrames = KanKan.NextFrames,
                LastFrames = KanKan.LastFrames
            };
        }

       
    }
}
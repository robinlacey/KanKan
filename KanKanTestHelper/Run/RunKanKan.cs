using System;
using KanKanCore.Karass;
using KanKanCore.Karass.Interface;
using KanKanTestHelper.Interface;
using KanKanTestHelper.Run.CurrentState;

namespace KanKanTestHelper.Run
{
    public class RunKanKan:IRunKanKan
    {
        public IRunUntil Until { get; }
        public IKanKan KanKan { get; }

        public RunKanKan(IKanKan kanKan, IRunUntil until)
        {
            Until = until;
            KanKan = kanKan;
        }

        public IKanKanCurrentState For(int frames)
        {
            KanKan.Reset();
            int frame = 0;
            for (int i = 0; i < frames; i++)
            {
               if (!KanKan.MoveNext())
               {
                   if (UncombinedKarass())
                   {
                       frame++;
                   }
                   break;
               }
               frame++;
            }

            return new KanKanCurrentState()
            {
                Frame = frame,
                NextFrames = KanKan.NextFrames,
                LastFrames = KanKan.LastFrames
            };
        }

        private bool UncombinedKarass()
        {
            KarassState karassState = (KarassState) KanKan.Current;
            return karassState.Karass.FramesCollection.Count == 1;
        }
    }
}
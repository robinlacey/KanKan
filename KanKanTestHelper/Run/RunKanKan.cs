using KanKanCore.Interface;
using KanKanCore.KanKan;
using KanKanCore.Karass;
using KanKanTestHelper.Interface;

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
            for (int i = 0; i < frames; i++)
            {
               
               if (!KanKan.MoveNext())
               {
                   break;
               }
            }

            KanKan test = ( KanKan) KanKan;
            return KanKan.GetCurrentState();
        }

        private bool UncombinedKarass()
        {
            KarassState karassState = (KarassState) KanKan.Current;
            return karassState.Karass.FramesCollection.Count == 1;
        }
    }
}
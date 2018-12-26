using KanKanCore.Interface;
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
            
            return KanKan.GetCurrentState();
        }
    }
}
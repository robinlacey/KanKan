using KanKanCore.Interface;
using KanKanTestHelper.Interface;

namespace KanKanTestHelper.Run
{
    public class RunKanKan:IRunKanKan
    {
        public IRunUntil Until { get; }

        public RunKanKan(IRunUntil until)
        {
            Until = until;
        }

        public IKanKanCurrentState For(int frames)
        {
            Until.KanKan.Reset();
            for (int i = 0; i < frames; i++)
            {
               if (!Until.KanKan.MoveNext())
               {
                   break;
               }
            }
            
            return Until.KanKan.Current;
        }
    }
}
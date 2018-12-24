using System;
using KanKanCore.Karass.Interface;
using KanKanTestHelper.Interface;
using KanKanTestHelper.Run.CurrentState;

namespace KanKanTestHelper.Run
{
    public class RunKanKan:IRunKanKan
    {
        private IKanKan _kanKan;

        public RunKanKan(IKanKan kanKan)
        {
            _kanKan = kanKan;
        }
        public IKanKanCurrentState For(int frames)
        {
            return new KanKanCurrentState()
            {
                NextFrames = _kanKan.NextFrames
            };
        }
    }
}
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
            _kanKan.Reset();
            
            for (int i = 0; i < frames; i++)
            {
                _kanKan.MoveNext();
            }

            return new KanKanCurrentState()
            {
                NextFrames = _kanKan.NextFrames,
                LastFrames = _kanKan.LastFrames
            };
        }
    }
}
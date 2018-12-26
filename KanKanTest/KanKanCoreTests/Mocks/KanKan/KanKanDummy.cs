using System.Collections.Generic;
using KanKanCore.Interface;
using KanKanCore.Karass.Frame;

namespace KanKanTest.KanKanCoreTests.Mocks.KanKan
{
    public class KanKanDummy:IKanKan
    {
        public bool MoveNext()
        {
            throw new System.NotImplementedException();
        }

        public void Reset()
        {
            throw new System.NotImplementedException();
        }

        public object Current { get; }
        public IFrameFactory FrameFactory { get; }
        public IKarassMessage KarassMessage { get; }
        
        public List<FrameRequest> NextFrames { get; }
        public List<FrameRequest> LastFrames { get; }

        public void SendMessage(string message)
        {
            throw new System.NotImplementedException();
        }

        public IKanKanCurrentState GetCurrentState()
        {
            throw new System.NotImplementedException();
        }
    }
}
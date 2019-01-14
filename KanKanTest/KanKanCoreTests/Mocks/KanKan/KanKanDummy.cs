using System.Collections.Generic;
using KanKanCore.Interface;
using KanKanCore.Karass.Frame;

namespace KanKanTest.KanKanCoreTests.Mocks.KanKan
{
    public class KanKanDummy:IKanKan
    {
        private IKanKanCurrentState _current;

        public bool MoveNext()
        {
            throw new System.NotImplementedException();
        }

        public void Reset()
        {
            throw new System.NotImplementedException();
        }

        IKanKanCurrentState IEnumerator<IKanKanCurrentState>.Current => _current;

        public object Current { get; }
        public IFrameFactory FrameFactory { get; }
        public IKarassMessage KarassMessage { get; }
        
        public List<FrameRequest> NextFrames { get; }
        public List<FrameRequest> LastFrames { get; }

        public string ID { get; }
        public List<IKarassState> AllKarassStates { get; }

        public void SendMessage(string message)
        {
            throw new System.NotImplementedException();
        }

        public void SetKarassMessage(IKarassMessage message)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}
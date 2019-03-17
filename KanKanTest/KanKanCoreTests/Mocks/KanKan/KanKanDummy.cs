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
            return true;
        }

        public void Reset()
        {
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
        }

        public void SetKarassMessage(IKarassMessage message)
        {
     
        }

        public void Dispose()
        {
         
        }
    }
}
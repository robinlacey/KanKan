using System.Collections.Generic;
using KanKanCore.Interface;
using KanKanCore.Karass.Frame;
using KanKanCore.Karass.Struct;

namespace KanKanTest.KanKanCoreTests.Mocks.KarassState
{
    public class KarassStateSpy: IKarassState
    {
        public int ResetCalledCount { get; private set; }
        public void Reset()
        {
            ResetCalledCount++;
        }

        public string ID { get; }
        public List<FrameRequest> NextFrames { get; }
        public List<FrameRequest> LastFrames { get; }
        public Dictionary<UniqueKarassFrameRequestID, int> CurrentFrames { get; }
        public List<bool> Complete { get; }
        public IKarass Karass { get; }
    }
}
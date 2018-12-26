using System.Collections.Generic;
using KanKanCore.Interface;
using KanKanCore.Karass.Frame;

namespace KanKanTest.KanKanTestHelperTests.Mocks
{
    public class KanKanCurrentStateDummy:IKanKanCurrentState
    {
        public IFrameFactory FrameFactory { get; }
        public IKarassMessage KarassMessage { get; }
        public int Frame { get; set; }
        public List<FrameRequest> NextFrames { get; set; }
        public List<FrameRequest> LastFrames { get; set; }
        public string NextMessage { get; }
        public string LastMessage { get; }
    }
}
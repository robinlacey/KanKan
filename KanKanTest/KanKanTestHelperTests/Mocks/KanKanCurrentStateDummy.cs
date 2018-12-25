using System.Collections.Generic;
using KanKanCore.Karass.Frame;
using KanKanTestHelper.Interface;

namespace KanKanTest.KanKanTestHelperTests.Mocks
{
    public class KanKanCurrentStateDummy:IKanKanCurrentState
    {
        public int Frame { get; set; }
        public List<FrameRequest> NextFrames { get; set; }
        public List<FrameRequest> LastFrames { get; set; }
    }
}
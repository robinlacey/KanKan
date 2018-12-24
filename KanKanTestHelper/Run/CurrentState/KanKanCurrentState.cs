using System.Collections.Generic;
using KanKanCore.Karass.Frame;
using KanKanTestHelper.Interface;

namespace KanKanTestHelper.Run.CurrentState
{
    public class KanKanCurrentState:IKanKanCurrentState
    {
        public List<FrameRequest> NextFrames { get; set; } = new List<FrameRequest>();
        public List<FrameRequest> LastFrames { get; set; } = new List<FrameRequest>();
    }
}
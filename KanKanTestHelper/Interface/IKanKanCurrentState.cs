using System.Collections.Generic;
using KanKanCore.Karass.Frame;

namespace KanKanTestHelper.Interface
{
    public interface IKanKanCurrentState
    {
        List<FrameRequest> NextFrames { get; set; }
        List<FrameRequest> LastFrames { get; set; }
    }
}
using System.Collections.Generic;
using KanKanCore.Karass.Frame;

namespace KanKanTestHelper.Interface
{
    public interface IKanKanCurrentState
    {
        int Frame { get; set; }
        List<FrameRequest> NextFrames { get; set; }
        List<FrameRequest> LastFrames { get; set; }
    }
}
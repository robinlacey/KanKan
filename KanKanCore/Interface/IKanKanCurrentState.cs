using System.Collections.Generic;
using KanKanCore.Karass.Frame;

namespace KanKanCore.Interface
{
    public interface IKanKanCurrentState
    {
        IFrameFactory FrameFactory { get; }
        IKarassMessage KarassMessage { get; }

        int Frame { get; set; }
        List<FrameRequest> NextFrames { get;  }
        List<FrameRequest> LastFrames { get; }
    }
}
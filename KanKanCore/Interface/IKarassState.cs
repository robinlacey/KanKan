using System.Collections.Generic;
using KanKanCore.Karass.Frame;
using KanKanCore.Karass.Struct;

namespace KanKanCore.Interface
{
    public interface IKarassState
    {
        void Reset();
        string ID { get; }
        List<FrameRequest> NextFrames { get; }
        List<FrameRequest> LastFrames { get; }
        Dictionary<UniqueKarassFrameRequestID, int> CurrentFrames { get; }
        List<bool> Complete { get; }
        IKarass Karass { get; }
    }
}
using System.Collections;
using System.Collections.Generic;
using KanKanCore.Karass.Frame;

namespace KanKanCore.Karass.Interface
{
    public interface IKanKan:IEnumerator
    {
        IFrameFactory FrameFactory { get; }
        IKarassMessage KarassMessage { get; }
        List<FrameRequest> NextFrames { get; }
        List<FrameRequest> LastFrames { get; }
        void SendMessage(string message);
    }
}
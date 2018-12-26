using System.Collections.Generic;
using KanKanCore.Interface;
using KanKanCore.Karass.Frame;

namespace KanKanCore.KanKan.CurrentState
{
    public class KanKanCurrentState:IKanKanCurrentState
    {
        public IFrameFactory FrameFactory { get;  set; }
        public IKarassMessage KarassMessage { get; set; }
        public int Frame { get; set; }
        public List<FrameRequest> NextFrames { get; set; }
        public List<FrameRequest> LastFrames { get; set; }
    }
}
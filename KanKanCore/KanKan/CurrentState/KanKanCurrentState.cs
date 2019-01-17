using System.Collections.Generic;
using KanKanCore.Interface;
using KanKanCore.Karass.Frame;

namespace KanKanCore.KanKan.CurrentState
{
    public struct KanKanCurrentState:IKanKanCurrentState
    {
        public IFrameFactory FrameFactory { get;  set; }
        public IKarassMessage KarassMessage { get; set; }
        public int TotalFramesRun { get; set; }
        public List<FrameRequest> NextFrames { get; set; }
        public List<FrameRequest> LastFrames { get; set; }
        public string NextMessage { get; set; }
        public string LastMessage { get; set; }
    }
}
using KanKanCore.Karass.Interface;
using KanKanTest.Mocks.KarassFrame.FrameStruct;

namespace KanKanTest.Mocks.KarassFrame
{
    class KarassFrameSpy : IKarassFrame<FrameStructDummy>
    {
        public FrameStructDummy RequestData { get; set; }
        public string Message { get; set; }
            
        public int ExecuteCallCount { get; private set; }
        public bool Execute(FrameStructDummy payload, string message)
        {
            ExecuteCallCount++;
            Message = message;
            RequestData = payload;
            return true;
        }
    }
}
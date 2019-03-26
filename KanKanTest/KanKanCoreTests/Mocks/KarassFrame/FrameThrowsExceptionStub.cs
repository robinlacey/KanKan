using System;
using KanKanCore.Interface;

namespace KanKanTest.KanKanCoreTests.Mocks.KarassFrame
{
    public class FrameThrowsExceptionStub:IKarassFrame<string>
    {
        private readonly Exception _e;

        public FrameThrowsExceptionStub(Exception e)
        {
            _e = e;
        }
        public IDependencies Dependencies { get; }
        public string RequestData { get; }
        public string Message { get; }
        public bool MoveNextFrame(string message, string payload)
        {
            throw _e;
        }
    }
}
using KanKanCore.Interface;
using KanKanCore.Karass.Frame;

namespace KanKanTest.KanKanCoreTests.Mocks.KarassFrame
{
    class KarassFrameSpy<T>:KarassFrame<T>
    {
        public int ExecuteCallCount { get; private set; }
        public override bool MoveNextFrame(string message, T payload)
        {
            Message = message;
            RequestData = payload;
            ExecuteCallCount++;
            return true;
        }

        public KarassFrameSpy(IDependencies dependencies) : base(dependencies)
        {
        }
    }
}
using KanKanCore.Karass.Frame;
namespace KanKanTest.Mocks.KarassFrame
{
    class KarassFrameSpy<T>:KarassFrame<T>
    {
        public int ExecuteCallCount { get; private set; }
        protected override bool ExecuteCustomLogic()
        {
            ExecuteCallCount++;
            return true;
        }
    }
}
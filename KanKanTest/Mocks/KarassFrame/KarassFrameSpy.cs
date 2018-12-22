using System;
using KanKanCore.Karass.Frame;
using KanKanCore.Karass.Interface;

namespace KanKanTest.Mocks.KarassFrame
{
    class KarassFrameSpy<T>:KarassFrame<T>
    {
        public int ExecuteCallCount { get; private set; }
        public override bool Execute(string message, T payload)
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
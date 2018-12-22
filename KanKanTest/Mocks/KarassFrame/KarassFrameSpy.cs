using System;
using KanKanCore.Karass.Frame;
using KanKanCore.Karass.Interface;

namespace KanKanTest.Mocks.KarassFrame
{
    class KarassFrameSpy<T>:KarassFrame<T>
    {
        public int ExecuteCallCount { get; private set; }
        public IDependencies Dependencies { get; private set; }
        public override bool Execute(string message, T payload, IDependencies dependencies)
        {
            Message = message;
            RequestData = payload;
            ExecuteCallCount++;
            Dependencies = dependencies;
            return true;
        }
    }
}
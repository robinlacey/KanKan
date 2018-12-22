using System;
using KanKanCore.Karass.Interface;

namespace KanKanCore.Karass.Frame.SimpleKarassFrame
{
    public class SimpleKarassFrame<T> : KarassFrame<T>
    {
        private readonly Func<string, bool> _simpleMethod;

        public override bool Execute(string message, T payload)
        {
            Message = message;
            return _simpleMethod.Invoke(message);
        }

        public SimpleKarassFrame(Func<string, bool> method, T requestData, IDependencies dependencies) : base(dependencies)
        {
            _simpleMethod = method;
            RequestData = requestData;
        }
        
        public SimpleKarassFrame(Func<string, bool> method, IDependencies dependencies) : base(dependencies)
        {
            _simpleMethod = method;
        }
    }
}
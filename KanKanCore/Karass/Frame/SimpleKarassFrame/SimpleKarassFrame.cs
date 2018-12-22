using System;
using KanKanCore.Karass.Interface;

namespace KanKanCore.Karass.Frame.SimpleKarassFrame
{
    public class SimpleKarassFrame<T> : IKarassFrame<T>
    {
        private readonly Func<string, bool> _simpleMethod;

        public SimpleKarassFrame(Func<string, bool> method, T requestData)
        {
            _simpleMethod = method;
            RequestData = requestData;
        }
        
        public SimpleKarassFrame(Func<string, bool> method)
        {
            _simpleMethod = method;
        }

        public T RequestData { get; }
        public string Message { get; set; }

        public bool Execute(string message, T payload)
        {
            Message = message;
            return _simpleMethod.Invoke(message);
        }
    }
}
using System;
using KanKanCore.Karass.Interface;

namespace KanKanCore.Karass.Frame.SimpleKarassFrame
{
    public class SimpleKarassFrame : IKarassFrame<object>
    {
        private Func<string, bool> _simpleMethod;

        public SimpleKarassFrame(Func<string, bool> method)
        {
            _simpleMethod = method;
            RequestData = new object();
        }

        public object RequestData { get; }
        public string Message { get; set; }

        public bool Execute(string message, object payload = null)
        {
            Message = message;
            return _simpleMethod.Invoke(message);
        }
    }
}
using KanKanCore.Karass.Interface;

namespace KanKanCore.Karass.Frame
{
    public abstract class KarassFrame<T> : IKarassFrame<T>
    {
        public T RequestData { get; private set; }
        public string Message { get; private set; }
        public bool Execute(string message, T payload)
        {
            RequestData = payload;
            Message = message;
            return ExecuteCustomLogic();
        }

        protected abstract bool ExecuteCustomLogic();
    }
}
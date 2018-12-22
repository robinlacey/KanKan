using KanKanCore.Karass.Interface;

namespace KanKanCore.Karass.Frame
{
    public abstract class KarassFrame<T> : IKarassFrame<T>
    {
        public T RequestData { get; protected set; }
        public string Message { get; protected set; }
        public abstract bool Execute(string message, T payload, IDependencies dependencies);

    }
}
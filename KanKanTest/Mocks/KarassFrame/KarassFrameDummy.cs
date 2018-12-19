using KanKanCore.Karass.Interface;

namespace KanKanTest.Mocks.KarassFrame
{
    public class KarassFrameDummy<T>:IKarassFrame<T>
    {
        public T RequestData { get; private set; }
        public string Message { get; private set; }

        public bool Execute(T payload, string message)
        {
            RequestData = payload;
            Message = message;
            return false;
        }
    }
}
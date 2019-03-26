using KanKanCore.Interface;

namespace KanKanCore.Karass.Frame
{
    public abstract class KarassFrame<T> : IKarassFrame<T>
    {
        public IDependencies Dependencies { get; }
        public T RequestData { get; protected set; }
        public string Message { get; protected set; }
  
        public abstract bool MoveNextFrame(string message, T payload);

        protected KarassFrame(IDependencies dependencies)
        {
            Dependencies = dependencies;
        }

    }
}
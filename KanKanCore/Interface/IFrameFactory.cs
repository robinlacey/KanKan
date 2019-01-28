using KanKanCore.Karass.Frame;

namespace KanKanCore.Interface
{
    public interface IFrameFactory
    {
        IDependencies Dependencies { get; }
        void RegisterRoute<TRequestType, TKarassFrameType>() where TKarassFrameType : IKarassFrame<TRequestType>;
        IKarassFrame<T> Get<T>();
        bool Execute(FrameRequest frameRequest, string message);
    }
}
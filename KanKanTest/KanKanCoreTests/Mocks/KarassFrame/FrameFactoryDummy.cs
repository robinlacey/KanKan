using KanKanCore.Interface;
using KanKanCore.Karass.Frame;

namespace KanKanTest.KanKanCoreTests.Mocks.KarassFrame
{
    public class FrameFactoryDummy: IFrameFactory
    {
        public IDependencies Dependencies { get; }

        public void RegisterRoute<TRequestType, TKarassFrameType>() where TKarassFrameType : IKarassFrame<TRequestType>
        {
            throw new System.NotImplementedException();
        }

        public IKarassFrame<T> Get<T>()
        {
            throw new System.NotImplementedException();
        }

        public bool Execute(FrameRequest frameRequest, string message)
        {
            return true;
        }
    }
}
using KanKanCore.Karass.Frame;
using KanKanCore.Karass.Interface;

namespace KanKanTest.Mocks.KarassFrame
{
    class KarassFrameDummy<T>:KarassFrame<T>
    {
        public override bool Execute(string message, T payload, IDependencies dependencies) => false;
    }
}
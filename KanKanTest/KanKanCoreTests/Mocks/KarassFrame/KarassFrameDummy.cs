using KanKanCore.Karass.Frame;
using KanKanCore.Karass.Interface;

namespace KanKanTest.KanKanCoreTests.Mocks.KarassFrame
{
    class KarassFrameDummy<T>:KarassFrame<T>
    {
        public override bool Execute(string message, T payload) => false;

        public KarassFrameDummy(IDependencies dependencies) : base(dependencies)
        {
           
        }
    }
}
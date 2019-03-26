using KanKanCore.Interface;
using KanKanCore.Karass.Frame;

namespace KanKanTest.KanKanCoreTests.Mocks.KarassFrame
{
    class KarassFrameDummy<T>:KarassFrame<T>
    {
        public override bool MoveToNextFrame(string message, T payload) =>true;

        public KarassFrameDummy(IDependencies dependencies) : base(dependencies)
        {
           
        }
    }
}
using KanKanCore.Karass.Frame;
namespace KanKanTest.Mocks.KarassFrame
{
    class KarassFrameDummy<T>:KarassFrame<T>
    {
        protected override bool ExecuteCustomLogic() => false;
    }
}
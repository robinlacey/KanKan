using KanKanCore.Karass.Interface;

namespace KanKanTestHelper.Interface
{
    public interface IRunKanKan
    {
        IKanKan KanKan { get; }
        IKanKanCurrentState For(int frames);
        IRunUntil Until { get; }
    }
}
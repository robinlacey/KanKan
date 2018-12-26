using KanKanCore.Interface;

namespace KanKanTestHelper.Interface
{
    public interface IRunKanKan
    {
        IKanKanCurrentState For(int frames);
        IRunUntil Until { get; }
    }
}
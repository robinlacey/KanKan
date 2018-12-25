using KanKanCore.Karass.Interface;

namespace KanKanTestHelper.Interface
{
    public interface IRunUntil
    {
        IKanKan KanKan { get; }
        IKanKanCurrentState LastFrame<T>(T requestEquals);
        IKanKanCurrentState NextFrame<T>(T requestEquals);
    }
}
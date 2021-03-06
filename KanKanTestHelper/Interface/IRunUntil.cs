using KanKanCore.Interface;

namespace KanKanTestHelper.Interface
{
    public interface IRunUntil
    {
        IKanKan KanKan { get; }
        IKanKanCurrentState LastFrame<T>(T requestEquals);
        IKanKanCurrentState NextFrame<T>(T requestEquals);
        IKanKanCurrentState HasReceived(string message);
        IKanKanCurrentState WillReceive(string message);
    }
}
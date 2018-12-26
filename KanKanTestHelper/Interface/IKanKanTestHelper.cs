using KanKanCore.Interface;

namespace KanKanTestHelper.Interface
{
    public interface IKanKanTestHelper
    {
        IFrameFactory FrameFactory { get; }
         IRunKanKan Run { get; }
    }
}
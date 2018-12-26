using KanKanCore.Interface;
using KanKanTestHelper.Interface;

namespace KanKanTestHelper
{
    public class TestHelper : IKanKanTestHelper
    {
        public IFrameFactory FrameFactory { get; }
        public IRunKanKan Run { get; }
        public TestHelper(IRunKanKan runKanKan, IFrameFactory frameFactory)
        {
            FrameFactory = frameFactory;
            Run = runKanKan;
        }
    }
}
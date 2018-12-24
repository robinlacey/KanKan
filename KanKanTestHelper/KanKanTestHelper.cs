using KanKanCore.Karass.Interface;
using KanKanTestHelper.Interface;

namespace KanKanTestHelper
{
    public class TestHelper : IKanKanTestHelper
    {
        public IFrameFactory FrameFactory { get; }
        public IRunKanKan Run { get; }
        public TestHelper(IRunKanKan runKanKan, IKanKan kankan)
        {
            FrameFactory = kankan.FrameFactory;
            Run = runKanKan;
        }
    }
}
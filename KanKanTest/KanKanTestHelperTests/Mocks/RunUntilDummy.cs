using KanKanCore.Karass.Interface;
using KanKanTestHelper.Interface;

namespace KanKanTest.KanKanTestHelperTests.Mocks
{
    public class RunUntilDummy:IRunUntil
    {
        public IKanKan KanKan { get; }
        public IKanKanCurrentState LastFrame<T>(T requestEquals)
        {
            throw new System.NotImplementedException();
        }

        public IKanKanCurrentState NextFrame<T>(T requestEquals)
        {
            throw new System.NotImplementedException();
        }
    }
}
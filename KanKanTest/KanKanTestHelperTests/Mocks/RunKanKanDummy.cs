using KanKanCore.Karass.Interface;
using KanKanTestHelper.Interface;

namespace KanKanTest.KanKanTestHelperTests.Mocks
{
    public class RunKanKanDummy: IRunKanKan
    {
        public IRunUntil Until { get; }
        public IKanKan KanKan { get; }

        public IKanKanCurrentState For(int frames)
        {
            return new KanKanCurrentStateDummy();
        }
    }
}